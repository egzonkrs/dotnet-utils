using System;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.BuildingBlocks.Messaging.Kafka.Abstractions;

/// <summary>
/// Generic Kafka Producer.
/// </summary>
/// <typeparam name="TValue">The value of the Kafka Message.</typeparam>
public interface IKafkaProducer<TValue> : IKafkaProducer<string, TValue>
{
}

/// <summary>
/// Generic Kafka Producer.
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
public interface IKafkaProducer<TKey, TValue> : IDisposable
{
    /// <summary>
    /// Send a single Message synchronously yo a Kafka Topic/Partition.
    /// </summary>
    /// <param name="kafkaMessage">The <see cref="IKafkaMessage{TKey,TValue}"/>.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns></returns>
    Task ProduceAsync(IKafkaMessage<TKey, TValue> kafkaMessage, CancellationToken cancellationToken = default);
}
