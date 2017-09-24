using System;
using System.Threading.Tasks;
using Common;
using Common.Log;

namespace Lykke.Job.LykkeJob.PeriodicalHandlers
{
    public class MyPeriodicalHandler : TimerPeriod
    {
        public MyPeriodicalHandler(ILog log) :
            // TODO: Sometimes, is enought to hardcode the period right here, but sometimes it's better to move it to the settings.
            // Do the simplest and enought decision
            base(nameof(MyPeriodicalHandler), (int)TimeSpan.FromSeconds(10).TotalMilliseconds, log)
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