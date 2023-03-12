using System;
using Microsoft.Extensions.Logging;
using SampleProject.BuildingBlocks.Messaging.Kafka.Abstractions;

namespace SampleProject.BuildingBlocks.Messaging.Kafka.Internal;

internal class KafkaProducerFactory : IKafkaProducerFactory
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly IMessageFactory _messageFactory;

    public KafkaProducerFactory(ILoggerFactory loggerFactory, IMessageFactory messageFactory)
    {
        _loggerFactory = loggerFactory;
        _messageFactory = messageFactory;
    }

    public IKafkaProducer<TKey, TValue> Create<TKey, TValue>(Action<KafkaProducerBuilderOptions<TKey, TValue>> setupAction)
    {
        var options = new KafkaProducerBuilderOptions<TKey, TValue>();
        setupAction?.Invoke(options);

        options.CreateTopicIfEnabled();

        return new KafkaProducer<TKey, TValue>(options, _loggerFactory.CreateLogger<KafkaProducer<TKey, TValue>>(), _messageFactory);
    }

    public IKafkaProducer<TValue> Create<TValue>(Action<KafkaProducerBuilderOptions<string, TValue>> setupAction)
    {
        var options = new KafkaProducerBuilderOptions<string, TValue>();
        setupAction?.Invoke(options);

        options.CreateTopicIfEnabled();

        return new KafkaProducer<TValue>(options, _loggerFactory.CreateLogger<KafkaProducer<TValue>>(), _messageFactory);
    }
}
