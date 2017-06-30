using System;

namespace Lykke.Job.LykkeJob.Models
{
    public class IsAliveResponse
    {
        public string Version { get; set; }
        public string Env { get; set; }

        // NOTE: Health status information example: 
        public DateTime LastFooStartedMoment { get; set; }
        public TimeSpan LastFooDuration { get; set; }
        public TimeSpan MaxHealthyFooDuration { get; set; }
    }
}