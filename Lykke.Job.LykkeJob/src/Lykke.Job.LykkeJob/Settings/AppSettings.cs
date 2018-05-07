using Lykke.Job.LykkeJob.Settings.JobSettings;
using Lykke.Job.LykkeJob.Settings.SlackNotifications;
using Lykke.SettingsReader.Attributes;

namespace Lykke.Job.LykkeJob.Settings
{
    public class AppSettings
    {
        public LykkeJobSettings LykkeJobJob { get; set; }

        public SlackNotificationsSettings SlackNotifications { get; set; }

        [Optional]
        public MonitoringServiceClientSettings MonitoringServiceClient { get; set; }
    }
}
