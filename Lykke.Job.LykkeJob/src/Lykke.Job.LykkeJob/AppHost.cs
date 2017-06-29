using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AzureStorage.Tables;
using Common.Log;
using Lykke.Job.LykkeJob.Core;
using Lykke.Job.LykkeJob.Modules;
using Lykke.JobTriggers.Triggers;
using Lykke.Logs;
using Lykke.SettingsReader;
using Lykke.SlackNotification.AzureQueue;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lykke.Job.LykkeJob
{
    public class AppHost
    {
        public static class EnvironmentNames
        {
            public static readonly string Development = "Development";
            public static readonly string Staging = "Staging";
            public static readonly string Production = "Production";
        }

        public static string HostingEnvironmentName => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        private IConfigurationRoot Configuration { get; }

        public AppHost(IConfigurationRoot configuration)
        {
            Configuration = configuration;
        }

        public void Run()
        {
            var builder = new ContainerBuilder();
            var appSettings = string.Equals(HostingEnvironmentName, EnvironmentNames.Development, StringComparison.OrdinalIgnoreCase)
                ? Configuration.Get<AppSettings>()
                : HttpSettingsLoader.Load<AppSettings>(Configuration.GetValue<string>("SettingsUrl"));
            var services = new ServiceCollection();
            var log = CreateLogWithSlack(services, appSettings);
            
            builder.RegisterModule(new JobModule(appSettings.AssetsService, log));
            builder.Populate(services);

            using (var jobContainer = builder.Build())
            {
                var triggerHost = new TriggerHost(new AutofacServiceProvider(jobContainer));

                triggerHost.Start().Wait();
            }
        }

        private ILog CreateLogWithSlack(ServiceCollection services, AppSettings settings)
        {
            LykkeLogToAzureStorage logToAzureStorage = null;

            var logToConsole = new LogToConsole();
            var logAggregate = new LogAggregate();

            logAggregate.AddLogger(logToConsole);

            var dbLogConnectionString = settings.AssetsService.Db.LogsConnString;

            // Creating azure storage logger, which logs own messages to concole log
            if (!string.IsNullOrEmpty(dbLogConnectionString) && !(dbLogConnectionString.StartsWith("${") && dbLogConnectionString.EndsWith("}")))
            {
                logToAzureStorage = new LykkeLogToAzureStorage("Lykke.Job.LykkeJob", new AzureTableStorage<LogEntity>(
                    dbLogConnectionString, "LykkeJobLog", logToConsole));

                logAggregate.AddLogger(logToAzureStorage);
            }

            // Creating aggregate log, which logs to console and to azure storage, if last one specified
            var log = logAggregate.CreateLogger();

            // Creating slack notification service, which logs own azure queue processing messages to aggregate log
            var slackService = services.UseSlackNotificationsSenderViaAzureQueue(new AzureQueueIntegration.AzureQueueSettings
            {
                ConnectionString = settings.SlackNotifications.AzureQueue.ConnectionString,
                QueueName = settings.SlackNotifications.AzureQueue.QueueName
            }, log);

            // Finally, setting slack notification for azure storage log, which will forward necessary message to slack service
            logToAzureStorage?.SetSlackNotification(slackService);

            return log;
        }
    }
}