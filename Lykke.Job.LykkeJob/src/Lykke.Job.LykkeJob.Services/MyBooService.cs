using System.Threading.Tasks;
using Lykke.Job.LykkeJob.Core.Services;

namespace Lykke.Job.LykkeJob.Services
{
    public class MyBooService : IMyBooService
    {
        public Task BooAsync()
        {
            return Task.FromResult(0);
        }
    }
}