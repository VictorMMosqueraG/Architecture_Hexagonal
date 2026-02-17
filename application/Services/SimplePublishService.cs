namespace Application.Services
{
    using Application.Interfaces.Services;
    using MessageBroker.Abstractions;

    public class SimplePublishService: ISimplePublishService
    {
        private readonly IMessageBrokerFactory _brokerFactory;

        public SimplePublishService(IMessageBrokerFactory brokerFactory)
        {
            _brokerFactory = brokerFactory ?? throw new ArgumentNullException(nameof(brokerFactory));
        }

        public async Task PublishToRabbitMQAsync(string message)
        {
            var rabbit = _brokerFactory.GetBroker(BrokerType.RabbitMQNamed("rabbitmqdefault"));
            if (rabbit != null)
                await rabbit.PublishAsync(message);
        }

        public async Task PublishToSQSAsync(string message)
        {
            var sqs = _brokerFactory.GetBroker(BrokerType.SQSNamed("documentmanager"));
            if (sqs != null)
                await sqs.PublishAsync(message);
        }

        public async Task PublishObjectAsync<T>(T obj, BrokerType brokerType) where T : class
        {
            var broker = _brokerFactory.GetBroker(brokerType);
            if (broker != null)
                await broker.PublishAsync(obj);
        }

        public async Task PublishToNamedBrokerAsync(string message, BrokerType brokerType)
        {
            var broker = _brokerFactory.GetBroker(brokerType);
            if (broker != null)
                await broker.PublishAsync(message);
        }
    }
}