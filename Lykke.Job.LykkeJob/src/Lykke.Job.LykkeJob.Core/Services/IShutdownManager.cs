using System.Threading.Tasks;
using Common;

namespace Lykke.Job.LykkeJob.Core.Services
{
    public interface IShutdownManager
    {
        Task StopAsync();

        void Register(IStopable stopable);
    }
}
