#if (examples)
using System;
#endif

namespace Lykke.Job.LykkeJob.Core.Services
{
    public interface IHealthService
    {
#if (examples)
        // NOTE: These are example properties
        DateTime LastFooStartedMoment { get; }
        TimeSpan LastFooDuration { get; }
        TimeSpan MaxHealthyFooDuration { get; }

        // NOTE: This method probably would stay in the real job, but will be modified
#endif
        string GetHealthViolationMessage();
        string GetHealthWarningMessage();
#if (examples)

        // NOTE: These are example methods
        void TraceFooStarted();
        void TraceFooCompleted();
        void TraceFooFailed();
        void TraceBooStarted();
        void TraceBooCompleted();
        void TraceBooFailed();
#endif
    }
}