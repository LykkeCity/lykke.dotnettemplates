using Lykke.Common;
using Lykke.Sdk;
using System.Threading.Tasks;

namespace Lykke.Service.LykkeService
{
    internal sealed class Program
    {
        public static async Task Main(string[] args)
        {
            await LykkeStarter.Start<Startup>(AppEnvironment.Name);
        }
    }
}
