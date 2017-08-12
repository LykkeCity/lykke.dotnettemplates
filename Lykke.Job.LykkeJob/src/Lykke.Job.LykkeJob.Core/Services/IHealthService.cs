using System.Collections.Generic;
using Lykke.Job.LykkeJob.Core.Domain.Health;

namespace Lykke.Job.LykkeJob.Core.Services
{
    // NOTE: See https://lykkex.atlassian.net/wiki/spaces/LKEWALLET/pages/35755585/Add+your+app+to+Monitoring
    public interface IHealthService
    {
        string GetHealthViolationMessage();
        IEnumerable<HealthIssue> GetHealthIssues();
#if (examples)

        // NOTE: These are example methods
        void TraceFooStarted();
        void TraceFooCompleted();
        void TraceFooFailed();
#endif
    }
}