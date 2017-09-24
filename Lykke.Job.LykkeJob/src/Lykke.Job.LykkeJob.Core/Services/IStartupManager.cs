using System.Threading.Tasks;

namespace Lykke.Job.LykkeJob.Core.Services
{
    public interface IStartupManager
    {
        Task StartAsync();
    }
}