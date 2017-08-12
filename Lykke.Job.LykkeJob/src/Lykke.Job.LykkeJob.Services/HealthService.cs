#if (examples)
using System;
#endif
using System.Collections.Generic;
using Lykke.Job.LykkeJob.Core.Domain.Health;
using Lykke.Job.LykkeJob.Core.Services;

namespace Lykke.Job.LykkeJob.Services
{
    // NOTE: See https://lykkex.atlassian.net/wiki/spaces/LKEWALLET/pages/35755585/Add+your+app+to+Monitoring
    public class HealthService : IHealthService
    {
#if (examples)
        // NOTE: These are example properties
        private DateTime LastFooStartedMoment { get; set; }
        private TimeSpan LastFooDuration { get; set; }
        private TimeSpan MaxHealthyFooDuration { get; }
        private bool WasLastFooFailed { get; set; }
        private bool WasLastFooCompleted { get; set; }
        private bool WasClientsFooEverStarted { get; set; }

        // NOTE: When you change parameters, don't forget to look in to JobModule

        public HealthService(TimeSpan maxHealthyFooDuration)
        {
            MaxHealthyFooDuration = maxHealthyFooDuration;
        }

        // NOTE: This method could stay in the real job, but will be modified
#endif
        public string GetHealthViolationMessage()
        {
            // TODO: Check gathered health statistics, and return appropriate health violation message, or NULL if job hasn't critical errors
            return null;
        }

        public IEnumerable<HealthIssue> GetHealthIssues()
        {
            var issues = new HealthIssuesCollection();

#if (examples)
            if (WasLastFooFailed)
            {
                issues.Add("LykkeJobFooProcessing", "Last foo was failed");
            }

            if (!WasLastFooCompleted && !WasLastFooFailed && !WasClientsFooEverStarted)
            {
                issues.Add("LykkeJobFooProcessingNotStartedYet", "Waiting for first foo execution started");
            }

            if (!WasLastFooCompleted && !WasLastFooFailed && WasClientsFooEverStarted)
            {
                issues.Add("LykkeJobFooProcessingNotCompletedYet", $"Waiting {DateTime.UtcNow - LastFooStartedMoment} for first foo execution completed");
            }

            if (LastFooDuration > MaxHealthyFooDuration)
            {
                issues.Add("LykkeJobFooProcessingLastedForToLong", $"Last foo was lasted for {LastFooDuration}, which is too long");
            }

#else
            // TODO: Check gathered health statistics, and add appropriate health issues message to issues
#endif
            return issues;
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
#endif
    }
}