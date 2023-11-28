using System;
using Confluent.Kafka;
using System.Collections.Generic;

namespace SampleProject.Utils.Messaging.Kafka.Abstractions;

/// <inheritdoc cref="IKafkaMessage{TKey,TValue}"/>
public class KafkaMessage<TValue> : KafkaMessage<string, TValue>, IKafkaMessage<TValue>
{
    /// <inheritdoc />
    public KafkaMessage(string messageType, string key, TValue value) : base(messageType, key, value)
    {
    }

    /// <inheritdoc />
    public KafkaMessage(string messageType, IDictionary<string, string> headers, string key, TValue value) : base(
        messageType, headers, key, value)
    {
    }

    /// <inheritdoc />
    public KafkaMessage(IDictionary<string, string> headers, string key, TValue value) : base(headers, key, value)
    {
    }

    /// <inheritdoc />
    public KafkaMessage(string key, TValue value) : base(key, value)
    {
    }
}

/// <inheritdoc cref="IKafkaMessage{TKey,TValue}"/>
public class KafkaMessage<TKey, TValue> : IKafkaMessage<TKey, TValue>
{
    /// <inheritdoc />
    public TKey Key { get; }

    /// <inheritdoc />
    public TValue Value { get; }

    /// <inheritdoc />
    public IDictionary<string, string> Headers { get; } = new Dictionary<string, string>();

    /// <inheritdoc />
    public bool ConfirmMessage { get; set; } = true;

    /// <inheritdoc />
    public string MessageType { get; }

    /// <inheritdoc />
    public string MessageId { get; set; } = Guid.NewGuid().ToString();

    /// <inheritdoc />
    public long? OffsetValue => PartitionOffset?.Offset.Value;

    /// <inheritdoc />
    public int? PartitionValue => PartitionOffset?.Partition.Value;

    /// <summary>
    /// Represents a Kafka (topic, partition, offset) tuple.
    /// </summary>
    public TopicPartitionOffset PartitionOffset { get; internal set; }

    /// <summary>
    /// The Constructor.
    /// </summary>
    /// <param name="messageType">The message type.</param>
    /// <param name="key">The partition key.</param>
    /// <param name="value">The message payload.</param>
    public KafkaMessage(string messageType, TKey key, TValue value)
    {
        MessageType = messageType;
        Key = key;
        Value = value;
    }

    /// <summary>
    /// The Constructor.
    /// </summary>
    /// <param name="messageType">The message type.</param>
    /// <param name="headers">The headers.</param>
    /// <param name="key">The partition key.</param>
    /// <param name="value">The message payload.</param>
    public KafkaMessage(string messageType, IDictionary<string, string> headers, TKey key, TValue value)
    {
        MessageType = messageType;
        Headers = headers;
        Key = key;
        Value = value;
    }

    /// <summary>
    /// The Constructor.
    /// </summary>
    /// <param name="headers">The headers.</param>
    /// <param name="key">The partition key.</param>
    /// <param name="value">The message payload.</param>
    public KafkaMessage(IDictionary<string, string> headers, TKey key, TValue value)
    {
        Headers = headers;
        Key = key;
        Value = value;
    }

    /// <summary>
    /// The Constructor.
    /// </summary>
    /// <param name="key">The partition key.</param>
    /// <param name="value">The message payload.</param>
    public KafkaMessage(TKey key, TValue value)
    {
        Key = key;
        Value = value;
    }
}