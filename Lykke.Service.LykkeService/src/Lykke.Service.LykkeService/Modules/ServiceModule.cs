using Autofac;

namespace Lykke.Service.LykkeService.Modules
{    
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Do not register entire settings in container, pass necessary settings to services which requires them
        }
    }
}
