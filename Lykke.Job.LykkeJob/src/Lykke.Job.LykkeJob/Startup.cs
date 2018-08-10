using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common.Log;
using JetBrains.Annotations;
using Lykke.Common;
using Lykke.Common.ApiLibrary.Middleware;
using Lykke.Common.ApiLibrary.Swagger;
using Lykke.Common.Api.Contract.Responses;
using Lykke.Common.Log;
using Lykke.Job.LykkeJob.Core.Services;
using Lykke.Job.LykkeJob.Settings;
using Lykke.Job.LykkeJob.Modules;
#if azurequeuesub
using Lykke.JobTriggers.Triggers;
#endif
using Lykke.Logs;
using Lykke.SettingsReader;
using Lykke.MonitoringServiceApiCaller;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using System;
using System.Threading.Tasks;

namespace Lykke.Job.LykkeJob
{
    [PublicAPI]
    public class Startup
    {
        private const string ApiVersion = "v1";
        private const string ApiName = "LykkeJob API";

        private string _monitoringServiceUrl;
        private ILog _log;
        private IHealthNotifier _healthNotifier;
#if azurequeuesub
        private TriggerHost _triggerHost;
        private Task _triggerHostTask;
#endif

        public IHostingEnvironment Environment { get; }
        public IContainer ApplicationContainer { get; private set; }
        public IConfigurationRoot Configuration { get; }

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
                        options.SerializerSettings.Converters.Add(new StringEnumConverter());
                        options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                    });

                services.AddSwaggerGen(options =>
                {
                    options.DefaultLykkeConfiguration(ApiVersion, ApiName);
                });

                var settingsManager = Configuration.LoadSettings<AppSettings>();

                var appSettings = settingsManager.CurrentValue;

                Configuration.CheckDependenciesAsync(
                    appSettings,
                    appSettings.SlackNotifications.AzureQueue.ConnectionString,
                    appSettings.SlackNotifications.AzureQueue.QueueName,
                    $"{AppEnvironment.Name} {AppEnvironment.Version}");

                if (appSettings.MonitoringServiceClient != null)
                    _monitoringServiceUrl = appSettings.MonitoringServiceClient.MonitoringServiceUrl;

                services.AddLykkeLogging(
                    settingsManager.ConnectionString(s => s.LykkeJobJob.Db.LogsConnString),
                    "LykkeJobLog",
                    appSettings.SlackNotifications.AzureQueue.ConnectionString,
                    appSettings.SlackNotifications.AzureQueue.QueueName);

                var builder = new ContainerBuilder();
                builder.Populate(services);

                builder.RegisterModule(new JobModule(appSettings.LykkeJobJob, settingsManager.Nested(x => x.LykkeJobJob)));

                ApplicationContainer = builder.Build();

                var logFactory = ApplicationContainer.Resolve<ILogFactory>();
                _log = logFactory.CreateLog(this);
                _healthNotifier = ApplicationContainer.Resolve<IHealthNotifier>();

                return new AutofacServiceProvider(ApplicationContainer);
            }
            catch (Exception ex)
            {
                if (_log == null)
                    Console.WriteLine(ex);
                else
                    _log.Critical(ex);
                throw;
            }
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            try
            {
                if (env.IsDevelopment())
                    app.UseDeveloperExceptionPage();

                app.UseLykkeForwardedHeaders();
                app.UseLykkeMiddleware(ex => new ErrorResponse {ErrorMessage = "Technical problem"});

                app.UseMvc();
                app.UseSwagger(c =>
                {
                    c.PreSerializeFilters.Add((swagger, httpReq) => swagger.Host = httpReq.Host.Value);
                });
                app.UseSwaggerUI(x =>
                {
                    x.RoutePrefix = "swagger/ui";
                    x.SwaggerEndpoint($"/swagger/{ApiVersion}/swagger.json", ApiVersion);
                });
                app.UseStaticFiles();

                appLifetime.ApplicationStarted.Register(() => StartApplication().GetAwaiter().GetResult());
                appLifetime.ApplicationStopping.Register(() => StopApplication().GetAwaiter().GetResult());
                appLifetime.ApplicationStopped.Register(CleanUp);
            }
            catch (Exception ex)
            {
                _log?.Critical(ex);
                throw;
            }
        }

        private async Task StartApplication()
        {
            try
            {
                // NOTE: Job not yet recieve and process IsAlive requests here

                await ApplicationContainer.Resolve<IStartupManager>().StartAsync();
#if azurequeuesub

                _triggerHost = new TriggerHost(new AutofacServiceProvider(ApplicationContainer));

                _triggerHostTask = _triggerHost.Start();
#endif
                _healthNotifier.Notify("Started", Program.EnvInfo);

//#$if !DEBUG
                await Configuration.RegisterInMonitoringServiceAsync(_monitoringServiceUrl, _healthNotifier);
//#$endif
            }
            catch (Exception ex)
            {
                _log.Critical(ex);
                throw;
            }
        }

        private async Task StopApplication()
        {
            try
            {
                // NOTE: Job still can recieve and process IsAlive requests here, so take care about it if you add logic here.

                await ApplicationContainer.Resolve<IShutdownManager>().StopAsync();
#if azurequeuesub

                _triggerHost?.Cancel();

                if(_triggerHostTask != null)
                    await _triggerHostTask;
#endif
            }
            catch (Exception ex)
            {
                _log?.Critical(ex);
                throw;
            }
        }

        private void CleanUp()
        {
            try
            {
                // NOTE: Job can't recieve and process IsAlive requests here, so you can destroy all resources
                _healthNotifier?.Notify("Terminating", Program.EnvInfo);

                ApplicationContainer.Dispose();
            }
            catch (Exception ex)
            {
                _log?.Critical(ex);
                throw;
            }
        }
    }
}
