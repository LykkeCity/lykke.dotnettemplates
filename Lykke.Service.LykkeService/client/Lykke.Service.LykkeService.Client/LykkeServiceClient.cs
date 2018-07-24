using Lykke.HttpClientGenerator;

namespace Lykke.Service.LykkeService.Client
{
    public class LykkeServiceClient : ILykkeServiceClient
    {
        //In case of several api controllers in your app - uncomment and use multiple APIs code below instead of one private field.
        private ILykkeServiceClient _api;

        //public IControllerApi1 Api1 { get; private set; }
        //public IControllerApi2 Api2 { get; private set; }

        public LykkeServiceClient(IHttpClientGenerator httpClientGenerator)
        {
            _api = httpClientGenerator.Generate<ILykkeServiceClient>();

            //Api1 = httpClientGenerator.Generate<IControllerApi1>();
            //Api2 = httpClientGenerator.Generate<IControllerApi2>();
        }
    }
}
