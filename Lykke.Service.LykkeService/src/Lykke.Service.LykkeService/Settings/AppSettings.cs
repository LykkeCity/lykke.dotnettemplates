using JetBrains.Annotations;
using Lykke.Service.LykkeService.Settings.ServiceSettings;
using Lykke.Service.LykkeService.Settings.SlackNotifications;
using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.LykkeService.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AppSettings
    {
        public LykkeServiceSettings LykkeServiceService { get; set; }

        public SlackNotificationsSettings SlackNotifications { get; set; }

        [Optional]
        public MonitoringServiceClientSettings MonitoringServiceClient { get; set; }
    }
}
