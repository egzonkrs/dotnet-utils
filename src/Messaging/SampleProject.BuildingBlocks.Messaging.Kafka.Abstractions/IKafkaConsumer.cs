using SampleProject.BuildingBlocks.Messaging.Abstractions;

namespace SampleProject.BuildingBlocks.Messaging.Kafka.Abstractions;

/// <summary>
/// Generic Kafka Consumer.
/// </summary>
public interface IKafkaConsumer : IKafkaConsumer<string, byte[]>
{
    /// <summary>
    /// Commits an offset based on the topic/partition/offset of a ConsumeResult.
    /// </summary>
    /// <param name="messages">The <see cref="IKafkaMessage{TKey,TValue}"/>.</param>
    void Commit<TValue>(params IMessage<TValue>[] messages);
}
