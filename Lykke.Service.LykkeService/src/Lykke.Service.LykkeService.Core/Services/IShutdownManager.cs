using System.Threading.Tasks;
using Common;

namespace Lykke.Service.LykkeService.Core.Services
{
    public interface IShutdownManager
    {
        Task StopAsync();

        void Register(IStopable stopable);
    }
}
