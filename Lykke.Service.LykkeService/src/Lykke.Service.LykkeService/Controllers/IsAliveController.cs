using System;
using Lykke.Service.LykkeService.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lykke.Service.LykkeService.Controllers
{
    [Route("api/[controller]")]
    public class IsAliveController : Controller
    {
        /// <summary>
        /// Checks service is alive
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IsAliveResponse Get()
        {
            return new IsAliveResponse
            {
                Version = Microsoft.Extensions.PlatformAbstractions.PlatformServices.Default.Application.ApplicationVersion,
                Env = Environment.GetEnvironmentVariable("Env")
            };
        }
    }
}
