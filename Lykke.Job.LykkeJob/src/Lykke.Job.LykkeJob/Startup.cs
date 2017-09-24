using System;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AzureStorage.Tables;
using Common.Log;
using Lykke.Common.ApiLibrary.Middleware;
using Lykke.Common.ApiLibrary.Swagger;
using Lykke.Job.LykkeJob.Core;
using Lykke.Job.LykkeJob.Models;
using Lykke.Job.LykkeJob.Modules;
using Lykke.JobTriggers.Extenstions;
using Lykke.JobTriggers.Triggers;
using Lykke.Logs;
using Lykke.SettingsReader;
using Lykke.SlackNotification.AzureQueue;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lykke.Job.LykkeJob
{
    public class Startup
    {
        public IHostingEnvironment Environment { get; }
        public IContainer ApplicationContainer { get; private set; }
        public IConfigurationRoot Configuration { get; }
        public ILog Log { get; private set; }

        private TriggerHost _triggerHost;
        private Task _triggerHostTask;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            Environment = env;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            try
            {
                services.AddMvc()
                    .AddJsonOptions(options =>
                    {
                        options.SerializerSettings.ContractResolver =
                            new Newtonsoft.Json.Serialization.DefaultContractResolver();
                    });

                services.AddSwaggerGen(options =>
                {
                    options.DefaultLykkeConfiguration("v1", "LykkeJob API");
                });

                var builder = new ContainerBuilder();
                var appSettings = Configuration.LoadSettings<AppSettings>();
                Log = CreateLogWithSlack(services, appSettings);

                builder.RegisterModule(new JobModule(appSettings.Nested(x => x.LykkeJobJob), Log));

                RegisterJobTriggers(appSettings.ConnectionString(x => x.LykkeJobJob.TriggerQueueConnectionString).CurrentValue, builder);

                builder.Populate(services);

                ApplicationContainer = builder.Build();

                return new AutofacServiceProvider(ApplicationContainer);
            }
            catch (Exception ex)
            {
                Log?.WriteFatalErrorAsync(nameof(Startup), nameof(ConfigureServices), "", ex);
                if (Log == null)
                {
                    Console.WriteLine(ex);
                }

                throw;
            }
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            try
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }

                app.UseLykkeMiddleware("LykkeJob", ex => new ErrorResponse {ErrorMessage = "Technical problem"});

                app.UseMvc();
                app.UseSwagger();
                app.UseSwaggerUi();
                app.UseStaticFiles();

                appLifetime.ApplicationStarted.Register(StartApplication);
                appLifetime.ApplicationStopping.Register(StopApplication);
                appLifetime.ApplicationStopped.Register(CleanUp);
            }
            catch (Exception ex)
            {
                Log?.WriteFatalErrorAsync(nameof(Startup), nameof(ConfigureServices), "", ex);
                if (Log == null)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private void StartApplication()
        {
            try
            {
                _triggerHost = new TriggerHost(new AutofacServiceProvider(ApplicationContainer));

                _triggerHostTask = _triggerHost.Start();
            }
            catch (Exception ex)
            {
                Log?.WriteFatalErrorAsync(nameof(Startup), nameof(StartApplication), "", ex);
                if (Log == null)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private void StopApplication()
        {
            try
            {
                // TODO: Implement your shutdown logic here. 
                // Job still can recieve and process IsAlive requests here, so take care about it.

                _triggerHost?.Cancel();
                _triggerHostTask?.Wait();
            }
            catch (Exception ex)
            {
                Log?.WriteFatalErrorAsync(nameof(Startup), nameof(StopApplication), "", ex);
                if (Log == null)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private void CleanUp()
        {
            try
            {
                // TODO: Implement your clean up logic here.
                // Job can't recieve and process IsAlive requests here, so you can destroy all resources

                ApplicationContainer.Dispose();
            }
            catch (Exception ex)
            {
                Log?.WriteFatalErrorAsync(nameof(Startup), nameof(CleanUp), "", ex);
                if (Log == null)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private static void RegisterJobTriggers(string connectionString, ContainerBuilder builder)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                builder.AddTriggers();
            }
            else
            {
                builder.AddTriggers(
                    pool =>
                    {
                        pool.AddDefaultConnection(connectionString);
                    });
            }
        }

        private static ILog CreateLogWithSlack(IServiceCollection services, IReloadingManager<AppSettings> settings)
        {
            var consoleLogger = new LogToConsole();
            var aggregateLogger = new AggregateLogger();

            aggregateLogger.AddLog(consoleLogger);

            // Creating slack notification service, which logs own azure queue processing messages to aggregate log
            var slackService = services.UseSlackNotificationsSenderViaAzureQueue(new AzureQueueIntegration.AzureQueueSettings
            {
                ConnectionString = settings.CurrentValue.SlackNotifications.AzureQueue.ConnectionString,
                QueueName = settings.CurrentValue.SlackNotifications.AzureQueue.QueueName
            }, aggregateLogger);

            var dbLogConnectionStringManager = settings.Nested(x => x.LykkeJobJob.Db.LogsConnString);
            var dbLogConnectionString = dbLogConnectionStringManager.CurrentValue;

            // Creating azure storage logger, which logs own messages to concole log
            if (!string.IsNullOrEmpty(dbLogConnectionString) && !(dbLogConnectionString.StartsWith("${") && dbLogConnectionString.EndsWith("}")))
            {
                const string appName = "Lykke.Job.LykkeJob";

                var persistenceManager = new LykkeLogToAzureStoragePersistenceManager(
                    appName,
                    AzureTableStorage<LogEntity>.Create(dbLogConnectionStringManager, "LykkeJobLog", consoleLogger),
                    consoleLogger);

                var slackNotificationsManager = new LykkeLogToAzureSlackNotificationsManager(appName, slackService, consoleLogger);

                var azureStorageLogger = new LykkeLogToAzureStorage(
                    appName,
                    persistenceManager,
                    slackNotificationsManager,
                    consoleLogger);

                azureStorageLogger.Start();

                aggregateLogger.AddLog(azureStorageLogger);
            }

            return aggregateLogger;
        }
    }
}