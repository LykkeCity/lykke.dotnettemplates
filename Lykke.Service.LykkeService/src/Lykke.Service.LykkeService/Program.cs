using Lykke.Sdk;
using System.Threading.Tasks;

namespace Lykke.Service.LykkeService
{
    internal sealed class Program
    {
        public static async Task Main(string[] args)
        {
//#$if DEBUG
            await LykkeStarter.Start<Startup>(true);
//#$else
            await LykkeStarter.Start<Startup>(false);
//#$endif
        }
    }
}
