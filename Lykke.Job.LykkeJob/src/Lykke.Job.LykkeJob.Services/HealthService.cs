using System;
using Lykke.Job.LykkeJob.Core.Services;

namespace Lykke.Job.LykkeJob.Services
{
    public class HealthService : IHealthService
    {
        public DateTime LastFooStartedMoment { get; private set; }
        public TimeSpan LastFooDuration { get; private set; }
        public TimeSpan MaxHealthyFooDuration { get; }

        private bool WasLastFooFailed { get; set; }
        private bool WasLastFooCompleted { get; set; }
        private bool WasClientsFooEverStarted { get; set; }

        public HealthService(TimeSpan maxHealthyFooDuration)
        {
            MaxHealthyFooDuration = maxHealthyFooDuration;
        }

        public string GetHealthViolationMessage()
        {
            if (WasLastFooFailed)
            {
                return "Last foo was failed";
            }

            if (!WasLastFooCompleted && !WasLastFooFailed && !WasClientsFooEverStarted)
            {
                return "Waiting for first foo execution started";
            }

            if (!WasLastFooCompleted && !WasLastFooFailed && WasClientsFooEverStarted)
            {
                return $"Waiting {DateTime.UtcNow - LastFooStartedMoment} for first foo execution completed";
            }

            if (LastFooDuration > MaxHealthyFooDuration)
            {
                return $"Last foo was lasted for {LastFooDuration}, which is too long";
            }

            return null;
        }

        public void TraceFooStarted()
        {
            LastFooStartedMoment = DateTime.UtcNow;
            WasClientsFooEverStarted = true;
        }

        public void TraceFooCompleted()
        {
            LastFooDuration = DateTime.UtcNow - LastFooStartedMoment;
            WasLastFooCompleted = true;
            WasLastFooFailed = false;
        }

        public void TraceFooFailed()
        {
            WasLastFooCompleted = false;
            WasLastFooFailed = true;
        }

        public void TraceBooStarted()
        {
            // TODO: See Foo
        }

        public void TraceBooCompleted()
        {
            // TODO: See Foo
        }

        public void TraceBooFailed()
        {
            // TODO: See Foo
        }
    }
}