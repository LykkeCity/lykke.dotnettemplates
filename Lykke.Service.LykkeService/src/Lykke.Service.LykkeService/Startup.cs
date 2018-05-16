using System;
using Lykke.Common.ApiLibrary.Swagger;
using Lykke.Sdk;
using Lykke.Service.LykkeService.Settings;
using Lykke.SettingsReader;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Lykke.Service.LykkeService
{
    public class Startup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver =
                        new Newtonsoft.Json.Serialization.DefaultContractResolver();
                });

            services.AddSwaggerGen(options =>
            {
                options.DefaultLykkeConfiguration("v1", "LykkeService API");
            });
            
            return services.BuildServiceProvider<AppSettings>(s => s.ConnectionString(x => x.LykkeServiceService.Db.LogsConnString));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseLykkeConfiguration(options =>
            {
                options.AppName = "LykkeService";
                options.Version = "v1";
#if (DEBUG)
                options.IsDebug = true;
#endif
            });
        }        
    }
}
