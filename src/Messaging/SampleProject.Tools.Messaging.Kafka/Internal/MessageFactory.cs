using System;
using Confluent.Kafka;
using SampleProject.Tools.Messaging.Kafka.Extensions;
using SampleProject.Tools.Messaging.Kafka.Abstractions;
using AbstractionConstants = SampleProject.Tools.Messaging.Kafka.Abstractions.Constants;

namespace SampleProject.Tools.Messaging.Kafka.Internal;

internal class MessageFactory : IMessageFactory
{
    public Message<TKey, byte[]> SerializeMessage<TKey, TValue>(
        IKafkaSerializer serializer,
        IKafkaMessage<TKey, TValue> kafkaMessage)
    {
        kafkaMessage.Headers.AddOrUpdate(AbstractionConstants.MessageTypeHeaderName, kafkaMessage.MessageType);
        kafkaMessage.Headers.AddOrUpdate(AbstractionConstants.MessageIdHeaderName, kafkaMessage.MessageId);

        return new Message<TKey, byte[]>
        {
            Key = kafkaMessage.Key,
            Value = serializer.Serialize(kafkaMessage.Value),
            Timestamp = new Timestamp(DateTime.UtcNow),
            Headers = kafkaMessage.Headers.ToKafkaHeaders()
        };
    }

    public KafkaMessage<TKey, TValue> DeserializeMessage<TKey, TValue>(
        IKafkaDeserializer deserializer,
        ConsumeResult<TKey, byte[]> result)
    {
        return DeserializeMessageInternal(result, deserializer.Deserialize<TValue>(result.Message.Value));
    }

    public KafkaMessage<TKey, byte[]> DeserializeMessage<TKey, TValue>(ConsumeResult<TKey, byte[]> result)
    {
        return DeserializeMessageInternal(result, result.Message.Value);
    }

    private KafkaMessage<TKey, TValue> DeserializeMessageInternal<TKey, TValue>(
        ConsumeResult<TKey, byte[]> result, TValue value)
    {
        return new KafkaMessage<TKey, TValue>(
            result.Message.Headers.GetHeaderValue(AbstractionConstants.MessageTypeHeaderName),
            result.Message.Headers.ToDictionary(),
            result.Message.Key,
            value)
        {
            MessageId = result.Message.Headers.GetHeaderValue(AbstractionConstants.MessageIdHeaderName),
            PartitionOffset = result.TopicPartitionOffset
        };
    }
}
