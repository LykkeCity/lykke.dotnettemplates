using Lykke.HttpClientGenerator;

namespace Lykke.Service.LykkeService.Client
{
    /// <summary>
    /// LykkeService API aggregating interface.
    /// </summary>
    public class LykkeServiceClient : ILykkeServiceClient
    {
        // Note: Add similar Api properties for each new service controller

        /// <summary>Inerface to LykkeService Api.</summary>
        public ILykkeServiceApi Api { get; private set; }

        /// <summary>C-tor</summary>
        public LykkeServiceClient(IHttpClientGenerator httpClientGenerator)
        {
            Api = httpClientGenerator.Generate<ILykkeServiceApi>();
        }
    }
}
