#if (examples)
using System;
#endif
using Lykke.Job.LykkeJob.Core.Services;

namespace Lykke.Job.LykkeJob.Services
{
    public class HealthService : IHealthService
    {
#if (examples)
        // NOTE: These are example properties
        public DateTime LastFooStartedMoment { get; private set; }
        public TimeSpan LastFooDuration { get; private set; }
        public TimeSpan MaxHealthyFooDuration { get; }

        // NOTE: These are example properties
        private bool WasLastFooFailed { get; set; }
        private bool WasLastFooCompleted { get; set; }
        private bool WasClientsFooEverStarted { get; set; }

        // NOTE: When you change parameters, don't forget to look in to JobModule

        public HealthService(TimeSpan maxHealthyFooDuration)
        {
            MaxHealthyFooDuration = maxHealthyFooDuration;
        }

        // NOTE: This method probably would stay in the real job, but will be modified
#endif
        public string GetHealthViolationMessage()
        {
#if (examples)
            if (WasLastFooFailed)
            {
                return "Last foo was failed";
            }
#else
            // TODO: Check gathered health statistics, and return appropriate health violation message, or NULL if job hasn't critical errors
#endif
            return null;
        }

        public string GetHealthWarningMessage()
        {
#if (examples)
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
#else
            // TODO: Check gathered health statistics, and return appropriate health warning message, or NULL if job is ok
#endif
            return null;
        }

#if (examples)

        // NOTE: These are example methods
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
#endif
    }
}