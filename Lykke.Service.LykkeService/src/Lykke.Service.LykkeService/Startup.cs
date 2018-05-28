using System;
using Autofac;
using Lykke.Sdk;
using Lykke.Service.LykkeService.Settings;
using Lykke.SettingsReader;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Lykke.Service.LykkeService
{
    public class Startup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {                                   
            return services.BuildServiceProvider<AppSettings>(options =>
            {
                options.ApiVersion = "v1";
                options.ApiTitle = "LykkeService API";
                options.LogsConnectionStringFactory = ctx => ctx.Resolve<IReloadingManager<AppSettings>>().ConnectionString(x => x.LykkeServiceService.Db.LogsConnString);
                options.LogsTableName = "LykkeServiceLog";
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseLykkeConfiguration(options =>
            {
#if (DEBUG)
                options.IsDebug = true;
#endif
            });
        }
    }
}
