using System.Collections.Generic;
using Lykke.Service.LykkeService.Core.Domain.Health;

namespace Lykke.Service.LykkeService.Core.Services
{
    // NOTE: See https://lykkex.atlassian.net/wiki/spaces/LKEWALLET/pages/35755585/Add+your+app+to+Monitoring
    public interface IHealthService
    {
        string GetHealthViolationMessage();
        IEnumerable<HealthIssue> GetHealthIssues();
    }
}