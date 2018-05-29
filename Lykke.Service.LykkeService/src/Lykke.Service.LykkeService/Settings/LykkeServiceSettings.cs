using JetBrains.Annotations;
using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.LykkeService.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class LykkeServiceSettings
    {
        public DbSettings Db { get; set; }
    }
}
