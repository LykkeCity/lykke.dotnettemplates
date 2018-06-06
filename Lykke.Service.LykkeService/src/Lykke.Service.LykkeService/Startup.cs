using System;
using Lykke.Sdk;
using Lykke.Service.LykkeService.Settings;
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
                options.ApiTitle = "LykkeService API";
                options.Logs = ("LykkeServiceLog", ctx => ctx.LykkeServiceService.Db.LogsConnString);
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseLykkeConfiguration();
        }
    }
}
