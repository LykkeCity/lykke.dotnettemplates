using Common;
using Lykke.Common.Log;
using System;
using System.Threading;
using System.Threading.Tasks;
using Autofac;

namespace Lykke.Job.LykkeService.PeriodicalHandlers
{
    public class MyPeriodicalHandler : IStartable, IStopable
    {
        private readonly TimerTrigger _timerTrigger;
        
        public MyPeriodicalHandler(ILogFactory logFactory)
        {
            // TODO: Sometimes, it is enough to hardcode the period right here, but sometimes it's better to move it to the settings.
            // Choose the simplest and sufficient solution
            _timerTrigger = new TimerTrigger(nameof(MyPeriodicalHandler), TimeSpan.FromSeconds(10), logFactory);
            _timerTrigger.Triggered += Execute;
        }

        public void Start()
        {
            _timerTrigger.Start();
        }
        
        public void Stop()
        {
            _timerTrigger.Stop();
        }

        public void Dispose()
        {
            _timerTrigger.Stop();
            _timerTrigger.Dispose();
        }

        private async Task Execute(ITimerTrigger timer, TimerTriggeredHandlerArgs args, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
    }
}
