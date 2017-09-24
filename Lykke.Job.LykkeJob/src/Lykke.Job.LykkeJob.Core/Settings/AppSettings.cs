using Lykke.Job.LykkeJob.Core.Settings.JobSettings;
using Lykke.Job.LykkeJob.Core.Settings.SlackNotifications;

namespace Lykke.Job.LykkeJob.Core.Settings
{
    public class AppSettings
    {
        public LykkeJobSettings LykkeJobJob { get; set; }
        public SlackNotificationsSettings SlackNotifications { get; set; }
    }
}