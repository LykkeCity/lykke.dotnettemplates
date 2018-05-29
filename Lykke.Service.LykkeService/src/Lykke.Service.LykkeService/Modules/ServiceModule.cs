using System;
using Autofac;
using Lykke.Sdk;
using Lykke.Service.LykkeService.Settings;
using Lykke.SettingsReader;
using Newtonsoft.Json.Linq;

namespace Lykke.Service.LykkeService.Modules
{    
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Do not register entire settings in container, pass necessary settings to services which requires them

            builder.RegisterSettings<ChaosKittySettings>("LykkeServiceService.ChaosKitty");
        }
    }
}
