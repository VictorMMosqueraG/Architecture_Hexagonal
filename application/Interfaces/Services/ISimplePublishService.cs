namespace Application.Interfaces.Services
{
    using MessageBroker.Abstractions;

    /// <summary>
    /// Service for publishing messages to message brokers (RabbitMQ, SQS)
    /// </summary>
    public interface ISimplePublishService
    {
        /// <summary>
        /// Publishes a text message to RabbitMQ
        /// </summary>
        /// <param name="message">The message to publish</param>
        Task PublishToRabbitMQAsync(string message);

        /// <summary>
        /// Publishes a text message to SQS
        /// </summary>
        /// <param name="message">The message to publish</param>
        Task PublishToSQSAsync(string message);

        /// <summary>
        /// Publishes an object to the specified broker type (serialized as JSON)
        /// </summary>
        /// <typeparam name="T">The type of object to publish</typeparam>
        /// <param name="obj">The object to publish</param>
        /// <param name="brokerType">The broker type (RabbitMQ or SQS)</param>
        Task PublishObjectAsync<T>(T obj, BrokerType brokerType) where T : class;

        /// <summary>
        /// Publishes a text message to the specified broker type
        /// </summary>
        /// <param name="message">The message to publish</param>
        /// <param name="brokerType">The broker type (RabbitMQ or SQS)</param>
        Task PublishToNamedBrokerAsync(string message, BrokerType brokerType);
    }
}
