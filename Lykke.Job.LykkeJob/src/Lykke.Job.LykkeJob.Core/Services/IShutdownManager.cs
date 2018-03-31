using System.Threading.Tasks;

namespace Lykke.Job.LykkeJob.Core.Services
{
    public interface IShutdownManager
    {
        Task StopAsync();
    }
}
