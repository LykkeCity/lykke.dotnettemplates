using System.Threading.Tasks;
using Lykke.Job.LykkeJob.Contract;
using Lykke.Job.LykkeJob.Core;
using Lykke.JobTriggers.Triggers.Attributes;

namespace Lykke.Job.LykkeJob.Functions
{
    // NOTE: This is the example functions class
    public class MyFunctions
    {
        private readonly AppSettings _appSettings;

        public MyFunctions(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        [TimerTrigger("00:00:10")]
        public async Task TimeTriggeredFunction()
        {
            await Task.FromResult(0);
        }

        [QueueTrigger("queue-name")]
        public async Task QueueTriggeredFunction(MyMessage msg)
        {
            await Task.FromResult(0);
        }
    }
}