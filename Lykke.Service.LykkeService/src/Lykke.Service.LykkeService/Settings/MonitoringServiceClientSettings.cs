using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.LykkeService.Settings
{
    public class MonitoringServiceClientSettings
    {
        [HttpCheck("api/isalive")]
        public string MonitoringServiceUrl { get; set; }
    }
}
