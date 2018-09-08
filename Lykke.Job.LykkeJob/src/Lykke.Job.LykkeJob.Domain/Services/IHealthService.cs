using System.Collections.Generic;
using Lykke.Common.Health;

namespace Lykke.Job.LykkeJob.Domain.Services
{
    // NOTE: See https://lykkex.atlassian.net/wiki/spaces/LKEWALLET/pages/35755585/Add+your+app+to+Monitoring
    public interface IHealthService
    {
        string GetHealthViolationMessage();
        IEnumerable<HealthIssue> GetHealthIssues();

        // TODO: Place health tracing methods declarations here
    }
}
