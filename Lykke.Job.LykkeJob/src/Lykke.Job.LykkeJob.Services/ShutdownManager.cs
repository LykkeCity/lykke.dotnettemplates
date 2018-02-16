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
        private readonly List<IStopable> _items = new List<IStopable>();

        public ShutdownManager(ILog log)
        {
            _log = log;
        }

        public void Register(IStopable stopable)
        {
            _items.Add(stopable);
        }

        public async Task StopAsync()
        {
            foreach (var item in _items)
            {
                item.Stop();
            }

            await Task.CompletedTask;
        }
    }
}
