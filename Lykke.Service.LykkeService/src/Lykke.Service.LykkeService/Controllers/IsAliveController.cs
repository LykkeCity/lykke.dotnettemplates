using Lykke.Common;
using Lykke.Common.Api.Contract.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Lykke.Service.LykkeService.Controllers
{
    // NOTE: See https://lykkex.atlassian.net/wiki/spaces/LKEWALLET/pages/35755585/Add+your+app+to+Monitoring
    [Route("api/[controller]")]
    public class IsAliveController : Controller
    {
        /// <summary>
        /// Checks service is alive
        /// </summary>
        [HttpGet]
        [SwaggerOperation("IsAlive")]
        [ProducesResponseType(typeof(IsAliveResponse), (int)HttpStatusCode.OK)]
        public IActionResult Get()
        {
            // NOTE: Feel free to extend IsAliveResponse, to display job-specific indicators
            return Ok(new IsAliveResponse
            {
                Name = AppEnvironment.Name,
                Version = AppEnvironment.Version,
                Env = AppEnvironment.EnvInfo,
                IsDebug = Debugger.IsAttached,
            });
        }
    }
}
