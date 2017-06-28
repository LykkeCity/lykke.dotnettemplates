using Autofac;
using AzureStorage.Tables;
using Common.Log;
using Lykke.Logs;
using Lykke.Service.LykkeService.Core;
using Microsoft.Extensions.PlatformAbstractions;

namespace Lykke.Service.LykkeService.Modules
{
    public class ServiceModule : Module
    {
        private readonly LykkeServiceSettings _settings;

        public ServiceModule(LykkeServiceSettings settings)
        {
            _settings = settings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_settings)
                .SingleInstance();

            var log = new LykkeLogToAzureStorage(PlatformServices.Default.Application.ApplicationName,
                new AzureTableStorage<LogEntity>(_settings.Db.LogsConnString, "LykkeServiceLog", null));

            builder.RegisterInstance(log)
                .As<ILog>()
                .SingleInstance();
        }
    }
}
