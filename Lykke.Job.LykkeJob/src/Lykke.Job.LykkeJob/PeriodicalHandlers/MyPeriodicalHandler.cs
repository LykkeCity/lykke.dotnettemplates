using Common;
using Lykke.Common.Log;
using System;
using System.Threading.Tasks;

namespace Lykke.Job.LykkeJob.PeriodicalHandlers
{
    public class MyPeriodicalHandler : TimerPeriod
    {
        public MyPeriodicalHandler(ILogFactory logFactory) :
            // TODO: Sometimes, it is enough to hardcode the period right here, but sometimes it's better to move it to the settings.
            // Choose the simplest and sufficient solution
            base(TimeSpan.FromSeconds(10), logFactory)
        {
        }

        public override async Task Execute()
        {
            // TODO: Orchestrate execution flow here and delegate actual business logic implementation to services layer
            // Do not implement actual business logic here

            await Task.CompletedTask;
        }
    }
}
