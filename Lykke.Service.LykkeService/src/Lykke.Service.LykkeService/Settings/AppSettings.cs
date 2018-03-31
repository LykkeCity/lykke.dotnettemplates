using JetBrains.Annotations;
using Lykke.Service.LykkeService.Settings.ServiceSettings;
using Lykke.Service.LykkeService.Settings.SlackNotifications;

namespace Lykke.Service.LykkeService.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AppSettings
    {
        public LykkeServiceSettings LykkeServiceService { get; set; }
        public SlackNotificationsSettings SlackNotifications { get; set; }
    }
}
