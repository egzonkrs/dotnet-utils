using System;
using System.Linq;
using System.Threading;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using SampleProject.Tools.Messaging.Kafka.Abstractions;

namespace SampleProject.Tools.Messaging.Kafka.Internal;

internal class KafkaConsumer<TKey, TValue> : IKafkaConsumer<TKey, TValue>
{
    private readonly IKafkaDeserializer _deserializer;
    private readonly IMessageFactory _messageFactory;
    private bool _disposed;

    internal readonly IConsumer<TKey, byte[]> Consumer;
    internal readonly ILogger<KafkaConsumer<TKey, byte[]>> Logger;
    internal readonly string Topic;

    internal KafkaConsumer(
        KafkaConsumerBuilderOptions<TKey, TValue> options,
        ILogger<KafkaConsumer<TKey, byte[]>> logger,
        IMessageFactory messageFactory)
    {
        Logger = logger;
        Topic = options.Topic;
        _messageFactory = messageFactory;

        var consumerBuilder = new ConsumerBuilder<TKey, byte[]>(options.KafkaOptions.ToConsumerProperties())
            .SetLogHandler((consumer, ex) => Logger.LogInformation(ex.Message))
            .SetErrorHandler((consumer, ex) =>
            {
                Logger.LogError(ex.Reason);
            })
            .SetPartitionsAssignedHandler((consumer, e) =>
            {
                Logger.LogDebug("Assigned partitions {partitions} for topic {topic}",
                    string.Join(", ", e.Select(x => x.Partition)), Topic);
            })
            .SetPartitionsRevokedHandler((_, e) =>
            {
                Logger.LogDebug("Revoked partitions {partitions} for topic {topic}",
                    string.Join(", ", e.Select(x => x.Partition)), Topic);
            });

        Consumer = consumerBuilder.Build();
        Consumer.Subscribe(Topic);

        _deserializer = options.ValueDeserializer;
    }

    public IKafkaMessage<TKey, TValue> Consume(CancellationToken cancellationToken = default)
    {
        try
        {
            var consumeResult = Consumer.Consume(cancellationToken);

            var kafkaMessage = DeserializeMessage(consumeResult);

            Logger.LogDebug("Consumed message key {key} at {partitionOffset} for topic {topic}",
                consumeResult.Message.Key,
                consumeResult.TopicPartitionOffset, Topic);

            return kafkaMessage;
        }
        catch (ConsumeException ex)
        {
            Logger.LogError(ex, "ConsumeException occurred {reason} for topic {topic}", ex.Error.Reason, Topic);
            throw;
        }
        catch (KafkaException ex)
        {
            Logger.LogError(ex, "KafkaException occurred {reason} for topic {topic}", ex.Error.Reason, Topic);
            throw;
        }
    }

    public void Commit(params IKafkaMessage<TKey, TValue>[] messages)
    {
        try
        {
            var offsets = messages.Cast<KafkaMessage<TKey, TValue>>().Select(msg =>
                new TopicPartitionOffset(msg.PartitionOffset.TopicPartition,
                    msg.PartitionOffset.Offset + 1));

            Consumer.Commit(offsets);

            Logger.LogDebug("Committed Kafka offsets {offsets} for topic {topicName}", offsets.Select(x => x.Offset),
                Topic);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Failed to commit Kafka offsets {offsets} for topic {topicName}",
                messages.Select(x => x.OffsetValue), Topic);
            throw;
        }
    }

    public void Dispose()
    {
        if (_disposed)
            return;

        Logger.LogDebug("Disposing Kafka Consumer {consumerName} for topic {topic}...", Consumer.Name, Topic);

        Consumer.Close();
        Consumer.Dispose();

        _disposed = true;
    }

    protected virtual KafkaMessage<TKey, TValue> DeserializeMessage(ConsumeResult<TKey, byte[]> consumeResult)
    {
        return _messageFactory.DeserializeMessage<TKey, TValue>(_deserializer, consumeResult);
    }
}
