using Confluent.Kafka;

namespace SampleProject.Tools.Messaging.Kafka.Abstractions;

/// <summary>
/// Provides a Factory to create an instance of <see cref="Message{TKey,TValue}"/>.
/// </summary>
public interface IMessageFactory
{
    /// <summary>
    /// Serializes a <see cref="IKafkaMessage{TKey,TValue}"/>.
    /// </summary>
    /// <param name="serializer">The <see cref="IKafkaSerializer"/>.</param>
    /// <param name="kafkaMessage">The <see cref="IKafkaMessage{TKey,TValue}"/>.</param>
    /// <typeparam name="TKey">The Key of the Kafka Message.</typeparam>
    /// <typeparam name="TValue">The Value of the Kafka Message.</typeparam>
    /// <returns>The <see cref="Message{TKey, TValue}"/>.</returns>
    Message<TKey, byte[]> SerializeMessage<TKey, TValue>(IKafkaSerializer serializer,
        IKafkaMessage<TKey, TValue> kafkaMessage);

    /// <summary>
    /// Deserializes a Kafka Message.
    /// </summary>
    /// <typeparam name="TKey">The Key of the Kafka Message.</typeparam>
    /// <typeparam name="TValue">The Value of the Kafka Message.</typeparam>
    /// <param name="deserializer">The <see cref="IKafkaDeserializer"/>.</param>
    /// <param name="result">The <see cref="ConsumeResult{TKey,TValue}"/>.</param>
    /// <returns>The <see cref="KafkaMessage{TValue}"/>.</returns>
    KafkaMessage<TKey, TValue> DeserializeMessage<TKey, TValue>(IKafkaDeserializer deserializer,
        ConsumeResult<TKey, byte[]> result);

    /// <summary>
    /// Deserializes a Kafka Message.
    /// </summary>
    /// <typeparam name="TKey">The Key of the Kafka Message.</typeparam>
    /// <typeparam name="TValue">The Value of the Kafka Message.</typeparam>
    /// <param name="result">The <see cref="ConsumeResult{TKey,TValue}"/>.</param>
    /// <returns>The <see><cref>KafkaMessage{TKey, byte[]}</cref></see></returns>
    KafkaMessage<TKey, byte[]> DeserializeMessage<TKey, TValue>(ConsumeResult<TKey, byte[]> result);
}