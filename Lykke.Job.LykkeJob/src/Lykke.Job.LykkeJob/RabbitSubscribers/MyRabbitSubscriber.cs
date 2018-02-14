using System;
using System.Threading.Tasks;
using Autofac;
using Common;
using Common.Log;
using Lykke.Job.LykkeJob.IncomingMessages;
using Lykke.RabbitMqBroker;
using Lykke.RabbitMqBroker.Subscriber;

namespace Lykke.Job.LykkeJob.RabbitSubscribers
{
    public class MyRabbitSubscriber : IStartable, IStopable
    {
        private readonly ILog _log;
        private readonly string _connectionString;
        private readonly string _exchangeName;
        private RabbitMqSubscriber<MySubscribedMessage> _subscriber;

        public MyRabbitSubscriber(
            ILog log,
            string connectionString,
            string exchangeName)
        {
            _log = log;
            _connectionString = connectionString;
            _exchangeName = exchangeName;
        }

        public void Start()
        {
            // NOTE: Read https://github.com/LykkeCity/Lykke.RabbitMqDotNetBroker/blob/master/README.md to learn
            // about RabbitMq subscriber configuration

            var settings = RabbitMqSubscriptionSettings
                .CreateForSubscriber(_connectionString, _exchangeName, "lykkejob");
            // TODO: Make additional configuration, using fluent API here:
            // ex: .MakeDurable()

            _subscriber = new RabbitMqSubscriber<MySubscribedMessage>(settings,
                    new ResilientErrorHandlingStrategy(_log, settings,
                        retryTimeout: TimeSpan.FromSeconds(10),
                        next: new DeadQueueErrorHandlingStrategy(_log, settings)))
                .SetMessageDeserializer(new JsonMessageDeserializer<MySubscribedMessage>())
                .Subscribe(ProcessMessageAsync)
                .CreateDefaultBinding()
                .SetLogger(_log)
                .SetConsole(new LogToConsole())
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
