using System.Threading.Tasks;
using Autofac;
using Common;
using Lykke.Job.LykkeJob.Contract;

namespace Lykke.Job.LykkeJob.Domain.Services
{
    public interface IMyRabbitPublisher : IStartable, IStopable
    {
        Task PublishAsync(MyPublishedMessage message);
    }
}