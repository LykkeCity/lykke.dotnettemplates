using System.Threading.Tasks;
using Lykke.Job.LykkeJob.Core.Services;

namespace Lykke.Job.LykkeJob.Services
{
    public class MyFooService : IMyFooService
    {
        public MyFooService(int someParameter)
        {
        }

        public Task FooAsync()
        {
            return Task.FromResult(0);
        }
    }
}