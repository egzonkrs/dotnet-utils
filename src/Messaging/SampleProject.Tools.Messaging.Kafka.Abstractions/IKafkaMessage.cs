using SampleProject.Tools.Messaging.Abstractions;

namespace SampleProject.Tools.Messaging.Kafka.Abstractions;

/// <summary>
/// Represents a Kafka message.
/// </summary>
/// <typeparam name="TValue"></typeparam>
public interface IKafkaMessage<TValue> : IKafkaMessage<string, TValue>, IMessage<TValue>
{
}

/// <summary>
/// Represents a Kafka message.
/// </summary>
/// <typeparam name="TKey">The message key value.</typeparam>
/// <typeparam name="TValue">The message value.</typeparam>
public interface IKafkaMessage<TKey, TValue> : IMessage<TKey, TValue>
{
    /// <summary>
    /// The message type.
    /// </summary>
    string MessageType { get; }

    /// <summary>
    /// The unique message id.
    /// </summary>
    string MessageId { get; }

    /// <summary>
    /// Partition Offset of the current message.
    /// </summary>
    long? OffsetValue { get; }

    /// <summary>
    /// Partition Value of the current message.
    /// </summary>
    int? PartitionValue { get; }
}
