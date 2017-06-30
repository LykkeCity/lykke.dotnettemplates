using System;

namespace Lykke.Job.LykkeJob.Core.Services
{
    public interface IHealthService
    {
        DateTime LastFooStartedMoment { get; }
        TimeSpan LastFooDuration { get; }
        TimeSpan MaxHealthyFooDuration { get; }

        string GetHealthViolationMessage();
        void TraceFooStarted();
        void TraceFooCompleted();
        void TraceFooFailed();
        void TraceBooStarted();
        void TraceBooCompleted();
        void TraceBooFailed();
    }
}