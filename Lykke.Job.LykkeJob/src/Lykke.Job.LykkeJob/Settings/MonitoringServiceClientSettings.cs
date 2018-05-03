using Lykke.SettingsReader.Attributes;

namespace Lykke.Job.LykkeJob.Settings
{
    public class MonitoringServiceClientSettings
    {
        [HttpCheck("api/isalive", false)]
        public string MonitoringServiceUrl { get; set; }
    }
}
