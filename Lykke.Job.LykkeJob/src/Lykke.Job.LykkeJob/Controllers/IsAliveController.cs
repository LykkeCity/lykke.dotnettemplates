using System;
using System.Net;
using Lykke.Job.LykkeJob.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.SwaggerGen.Annotations;

namespace Lykke.Job.LykkeJob.Controllers
{
    [Route("api/[controller]")]
    public class IsAliveController : Controller
    {
        /// <summary>
        /// Checks service is alive
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation("IsAlive")]
        [ProducesResponseType(typeof(IsAliveResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public IActionResult Get()
        {
            // TODO: Check job health status here, if job unhealthy, send ErrorResponse
            // if (!isHealthy)
            // {
            //     return StatusCode((int) HttpStatusCode.InternalServerError, new ErrorResponse
            //     {
            //         ErrorMessage = "Problem description"
            //     });
            // }

            // NOTE: Feel free to extend IsAliveResponse, to display job-specific health status
            return Ok(new IsAliveResponse
            {
                Version = Microsoft.Extensions.PlatformAbstractions.PlatformServices.Default.Application.ApplicationVersion,
                Env = Environment.GetEnvironmentVariable("Env")
            });
        }
    }
}