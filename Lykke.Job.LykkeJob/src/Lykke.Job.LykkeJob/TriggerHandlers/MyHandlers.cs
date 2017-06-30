using System.Threading.Tasks;
using Lykke.Job.LykkeJob.Contract;
using Lykke.Job.LykkeJob.Core;
using Lykke.JobTriggers.Triggers.Attributes;

namespace Lykke.Job.LykkeJob.TriggerHandlers
{
    // NOTE: This is the example trigger handlers class
    public class MyHandlers
    {
        // NOTE: The object is instantiated using DI container, so registered dependencies are injected well
        public MyHandlers(AppSettings.LykkeJobSettings appSettings)
        {
        }

        [TimerTrigger("00:00:10")]
        public async Task TimeTriggeredHandler()
        {
            await Task.FromResult(0);
        }

        [QueueTrigger("queue-name")]
        public async Task QueueTriggeredHandler(MyMessage msg)
        {
            await Task.FromResult(0);
        }
    }
}