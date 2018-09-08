using System.Threading.Tasks;

namespace Lykke.Job.LykkeJob.Domain.Services
{
    public interface IStartupManager
    {
        Task StartAsync();
    }
}