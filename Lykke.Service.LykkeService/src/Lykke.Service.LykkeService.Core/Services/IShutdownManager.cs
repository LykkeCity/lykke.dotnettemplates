using System.Threading.Tasks;

namespace Lykke.Service.LykkeService.Core.Services
{
    public interface IShutdownManager
    {
        Task StopAsync();
    }
}
