using Lykke.Job.LykkeService.Settings.JobSettings;
using Lykke.Job.LykkeService.Settings.SlackNotifications;
using Lykke.SettingsReader.Attributes;

namespace Lykke.Job.LykkeService.Settings
{
    public class AppSettings
    {
        public LykkeServiceJobSettings LykkeServiceJob { get; set; }

        public SlackNotificationsSettings SlackNotifications { get; set; }

        [Optional]
        public MonitoringServiceClientSettings MonitoringServiceClient { get; set; }
    }
}
