using System;
using JetBrains.Annotations;
using Lykke.Sdk;
using Lykke.Service.LykkeService.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.DependencyInjection;

namespace Lykke.Service.LykkeService
{
    [UsedImplicitly]
    public class Startup
    {
        [UsedImplicitly]
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {                                   
            return services.BuildServiceProvider<AppSettings>(options =>
            {
                options.ApiTitle = "LykkeService API";
                options.Logs = logs =>
                {
                    logs.AzureTableName = "LykkeServiceLog";
                    logs.AzureTableConnectionStringResolver = settings => settings.LykkeServiceService.Db.LogsConnString;

                    // TODO: You could add extended logging configuration here:
                    /* 
                    logging.Extended = extendedLogging =>
                    {
                        // For example, you could add additional slack channel like this:
                        extendedLogging.AddAdditionalSlackChannel("LykkeService", channelOptions =>
                        {
                            channelOptions.MinLogLevel = LogLevel.Information;
                        });
                    };
                    */
                };

                // TODO: You could add extended Swagger configuration here:
                /*
                options.Swagger = swagger =>
                {
                    swagger.IgnoreObsoleteActions();
                }
                */
            });
        }

        [UsedImplicitly]
        public void Configure(IApplicationBuilder app)
        {
            app.UseLykkeConfiguration();

#if DEBUG
            TelemetryConfiguration.Active.DisableTelemetry = true;
#endif
        }
    }
}
