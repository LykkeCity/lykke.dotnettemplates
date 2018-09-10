using Autofac;
using Common;
using Common.Log;
using Lykke.Common.Log;
using Lykke.Job.LykkeService.IncomingMessages;
using Lykke.RabbitMqBroker;
using Lykke.RabbitMqBroker.Subscriber;
using System;
using System.Threading.Tasks;

namespace Lykke.Job.LykkeService.RabbitSubscribers
{
    public class MyRabbitSubscriber : IStartable, IStopable
    {
        private readonly ILogFactory _logFactory;
        private readonly string _connectionString;
        private readonly string _exchangeName;
        private RabbitMqSubscriber<MySubscribedMessage> _subscriber;

        public MyRabbitSubscriber(
            ILogFactory logFactory,
            string connectionString,
            string exchangeName)
        {
            _logFactory = logFactory;
            _connectionString = connectionString;
            _exchangeName = exchangeName;
        }

        public void Start()
        {
            // NOTE: Read https://github.com/LykkeCity/Lykke.RabbitMqDotNetBroker/blob/master/README.md to learn
            // about RabbitMq subscriber configuration

            var settings = RabbitMqSubscriptionSettings
                .CreateForSubscriber(_connectionString, _exchangeName, "lykkeservicejob");
            // TODO: Make additional configuration, using fluent API here:
            // ex: .MakeDurable()

            _subscriber = new RabbitMqSubscriber<MySubscribedMessage>(
                    _logFactory,
                    settings,
                    new ResilientErrorHandlingStrategy(
                        _logFactory,
                        settings,
                        TimeSpan.FromSeconds(10),
                        next: new DeadQueueErrorHandlingStrategy(_logFactory, settings)))
                .SetMessageDeserializer(new JsonMessageDeserializer<MySubscribedMessage>())
                .Subscribe(ProcessMessageAsync)
                .CreateDefaultBinding()
                .Start();
        }

        private async Task ProcessMessageAsync(MySubscribedMessage arg)
        {
            // TODO: Orchestrate execution flow here and delegate actual business logic implementation to services layer
            // Do not implement actual business logic here

            await Task.CompletedTask;
        }

        public void Dispose()
        {
            _subscriber?.Dispose();
        }

        public void Stop()
        {
            _subscriber?.Stop();
        }
    }
}
