using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Common.Log;
using Lykke.Common.Log;
#if azurequeuesub
using Lykke.JobTriggers.Triggers;
#endif
using Lykke.Sdk;

namespace Lykke.Job.LykkeService.Services
{
    // NOTE: Sometimes, shutdown process should be expressed explicitly. 
    // If this is your case, use this class to manage shutdown.
    // For example, sometimes some state should be saved only after all incoming message processing and 
    // all periodical handler was stopped, and so on.
    public class ShutdownManager : IShutdownManager
    {
        private readonly ILog _log;
        private readonly IEnumerable<IStopable> _items;
#if azurequeuesub
        private readonly TriggerHost _triggerHost;
#endif

        public ShutdownManager(
            ILogFactory logFactory, 
#if azurequeuesub
            TriggerHost triggerHost,
#endif
            IEnumerable<IStopable> items)
        {
            _log = logFactory.CreateLog(this);
            _items = items;
#if azurequeuesub
            _triggerHost = triggerHost;
#endif
        }

        public async Task StopAsync()
        {
            // TODO: Implement your shutdown logic here. Good idea is to log every step
            foreach (var item in _items)
            {
                try
                {
                    item.Stop();
                }
                catch (Exception ex)
                {
                    _log.Warning($"Unable to stop {item.GetType().Name}", ex);
                }
            }
            
#if azurequeuesub
            _triggerHost.Cancel();
#endif
            await Task.CompletedTask;
        }
    }
}
