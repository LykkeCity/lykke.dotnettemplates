using System.Threading.Tasks;
using Autofac;
using Common;
using Lykke.Job.LykkeService.Contract;

namespace Lykke.LykkeType.LykkeService.Domain.Services
{
    public interface IMyRabbitPublisher : IStartable, IStopable
    {
        Task PublishAsync(MyPublishedMessage message);
    }
}