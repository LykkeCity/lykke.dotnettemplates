using Lykke.Job.LykkeService.Settings.JobSettings;
using Lykke.Sdk.Settings;

namespace Lykke.Job.LykkeService.Settings
{
    public class AppSettings : BaseAppSettings
    {
        public LykkeServiceJobSettings LykkeServiceJob { get; set; }
    }
}
