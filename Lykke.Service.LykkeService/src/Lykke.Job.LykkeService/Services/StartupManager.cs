using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
#if azurequeuesub
using Lykke.JobTriggers.Triggers;
#endif
using Lykke.Sdk;

namespace Lykke.Job.LykkeService.Services
{
    // NOTE: Sometimes, startup process which is expressed explicitly is not just better, 
    // but the only way. If this is your case, use this class to manage startup.
    // For example, sometimes some state should be restored before any periodical handler will be started, 
    // or any incoming message will be processed and so on.
    // Do not forget to remove As<IStartable>() and AutoActivate() from DI registartions of services, 
    // which you want to startup explicitly.

    public class StartupManager : IStartupManager
    {
#if azurequeuesub
        private readonly ILog _log;
        private readonly TriggerHost _triggerHost;

        public StartupManager(
            ILogFactory logFactory,
            TriggerHost triggerHost)
        {
            _log = logFactory.CreateLog(this);
            _triggerHost = triggerHost;
        }

        public async Task StartAsync()
        {
            await _triggerHost.Start();
        }
#else
        private readonly ILog _log;

        public StartupManager(ILogFactory logFactory)
        {
            _log = logFactory.CreateLog(this);
        }

        public async Task StartAsync()
        {
            // TODO: Implement your startup logic here. Good idea is to log every step

            await Task.CompletedTask;
        }
#endif
    }
}
