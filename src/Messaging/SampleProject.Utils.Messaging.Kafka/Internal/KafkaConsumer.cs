using System;
using System.Linq;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using SampleProject.Utils.Messaging.Abstractions;
using SampleProject.Utils.Messaging.Kafka.Abstractions;

namespace SampleProject.Utils.Messaging.Kafka.Internal;

internal class KafkaConsumer : KafkaConsumer<string, byte[]>, IKafkaConsumer
{
    private readonly IMessageFactory _messageFactory;

    public KafkaConsumer(
        KafkaConsumerBuilderOptions<string, byte[]> options,
        ILogger<KafkaConsumer<string, byte[]>> logger,
        IMessageFactory messageFactory)
        : base(options, logger, messageFactory)
    {
        _messageFactory = messageFactory;
    }

    protected override KafkaMessage<string, byte[]> DeserializeMessage(ConsumeResult<string, byte[]> consumeResult)
    {
        return _messageFactory.DeserializeMessage<string, byte[]>(consumeResult); // TODO: check if this is correct
    }

    public void Commit<TValue>(params IMessage<TValue>[] messages)
    {
        try
        {
            var offsets = messages.Cast<KafkaMessage<TValue>>().Select(msg =>
                new TopicPartitionOffset(msg.PartitionOffset.TopicPartition,
                    msg.PartitionOffset.Offset + 1));

            Consumer.Commit(offsets);

            Logger.LogDebug("Committed Kafka offsets {offsets} for topic {topicName}",
                offsets.Select(x => x.Offset), Topic);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Failed to commit Kafka offsets {offsets} for topic {topicName}",
                messages.Cast<KafkaMessage<TValue>>().Select(x => x.OffsetValue), Topic);
            throw;
        }
    }
}
