using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common.Log;
using Lykke.Job.LykkeJob.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Lykke.Job.LykkeJob.Modules
{
    public class JobModule : Module
    {
        private readonly AppSettings.LykkeJobSettings _settings;
        private readonly ILog _log;
        // NOTE: you can remove it if you don't need to use IServiceCollection extensions to register service specific dependencies
        private readonly IServiceCollection _services;

        public JobModule(AppSettings.LykkeJobSettings settings, ILog log)
        {
            _settings = settings;
            _log = log;

            _services = new ServiceCollection();
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_settings)
                .SingleInstance();

            builder.RegisterInstance(_log)
                .As<ILog>()
                .SingleInstance();

            // TODO: Add your dependencies here

            builder.Populate(_services);
        }
    }
}