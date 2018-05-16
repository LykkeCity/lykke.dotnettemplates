using JetBrains.Annotations;
using Lykke.Sdk.Settings;

namespace Lykke.Service.LykkeService.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AppSettings : BaseAppSettings
    {
        public LykkeServiceSettings LykkeServiceService { get; set; }        
    }
}
