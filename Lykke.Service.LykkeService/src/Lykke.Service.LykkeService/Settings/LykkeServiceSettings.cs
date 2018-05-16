using System.Collections.Generic;
using JetBrains.Annotations;

namespace Lykke.Service.LykkeService.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class LykkeServiceSettings
    {
        public DbSettings Db { get; set; }
    }
}
