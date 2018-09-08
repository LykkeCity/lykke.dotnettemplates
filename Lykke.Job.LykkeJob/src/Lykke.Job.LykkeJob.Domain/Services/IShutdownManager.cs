using System.Threading.Tasks;

namespace Lykke.Job.LykkeJob.Domain.Services
{
    public interface IShutdownManager
    {
        Task StopAsync();
    }
}
