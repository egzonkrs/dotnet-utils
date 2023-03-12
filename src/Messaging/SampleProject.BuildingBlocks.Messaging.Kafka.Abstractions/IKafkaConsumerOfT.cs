using System;
using System.Threading;

namespace SampleProject.BuildingBlocks.Messaging.Kafka.Abstractions;

/// <summary>
/// Kafka Consumer configured by a key-value pair.
/// </summary>
/// <typeparam name="TKey">The Key of the message.</typeparam>
/// <typeparam name="TValue">The Value of the message.</typeparam>
public interface IKafkaConsumer<TKey, TValue> : IDisposable
{
    /// <summary>
    /// Poll for new messages or events.
    /// Blocks until a consume result is available or the operation has been cancelled.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    IKafkaMessage<TKey, TValue> Consume(CancellationToken cancellationToken = default);

    /// <summary>
    /// Commits an offset based on the topic/partition/offset of a ConsumeResult.
    /// </summary>
    /// <param name="messages">The <see cref="IKafkaMessage{TKey,TValue}"/>.</param>
    void Commit(params IKafkaMessage<TKey, TValue>[] messages);
}
