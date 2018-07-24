using Lykke.HttpClientGenerator;

namespace Lykke.Service.LykkeService.Client
{
    public class LykkeServiceClient : ILykkeServiceClient
    {
        //In case of several api controllers in your app - uncomment and use multiple APIs code below.
        private ILykkeServiceClient _api;

        //private IControllerApi1 _api1;
        //private IControllerApi2 _api2;

        public LykkeServiceClient(IHttpClientGenerator httpClientGenerator)
        {
            _api = httpClientGenerator.Generate<ILykkeServiceClient>();

            //_api1 = httpClientGenerator.Generate<IControllerApi1>();
            //_api2 = httpClientGenerator.Generate<IControllerApi2>();
        }
    }
}
