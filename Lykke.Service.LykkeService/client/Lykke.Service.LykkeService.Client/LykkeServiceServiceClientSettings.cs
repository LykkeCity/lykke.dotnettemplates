using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.LykkeService.Client 
{
    /// <summary>
    /// LykkeService client settings.
    /// </summary>
    public class LykkeServiceServiceClientSettings 
    {
        /// <summary>Service url.</summary>
        [HttpCheck("api/isalive")]
        public string ServiceUrl {get; set;}
    }
}
