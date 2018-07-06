using Lykke.HttpClientGenerator;

namespace Lykke.Service.LykkeService.Client
{
    public class LykkeServiceClient : ILykkeServiceClient
    {
        //public IControllerApi Controller { get; }
        
        public LykkeServiceClient(IHttpClientGenerator httpClientGenerator)
        {
            //Controller = httpClientGenerator.Generate<IControllerApi>();
        }
        
    }
}
