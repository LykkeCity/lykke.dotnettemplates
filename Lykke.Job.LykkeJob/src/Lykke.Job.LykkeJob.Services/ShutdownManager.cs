using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Common.Log;
using Lykke.Job.LykkeJob.Core.Services;

namespace Lykke.Job.LykkeJob.Services
{
    // NOTE: Sometimes, shutdown process should be expressed explicitly. 
    // If this is your case, use this class to manage shutdown.
    // For example, sometimes some state should be saved only after all incoming message processing and 
    // all periodical handler was stopped, and so on.
    
    public class ShutdownManager : IShutdownManager
    {
        private readonly ILog _log;
        private readonly IEnumerable<IStopable> _items;

        public ShutdownManager(ILog log, IEnumerable<IStopable> items)
        {
            _log = log;
            _items = items;
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
                    _log.WriteWarning(nameof(StopAsync), null, $"Unable to stop {item.GetType().Name}", ex);
                }
            }

            await Task.CompletedTask;
        }
    }
}
