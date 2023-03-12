using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SampleProject.BuildingBlocks.Messaging.Kafka.Abstractions;

namespace SampleProject.BuildingBlocks.Messaging.Kafka.Internal;

internal class KafkaConsumerFactory //: IKafkaConsumerFactory
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly IOptionsMonitor<KafkaConsumerBuilderOptions<string, byte[]>> _kafkaTopicOptionsMonitor;
    private readonly IMessageFactory _messageFactory;
    private readonly Dictionary<string, object> _instances = new Dictionary<string, object>();
    private readonly object _instancesLock = new object();

    public KafkaConsumerFactory(ILoggerFactory loggerFactory,
        IOptionsMonitor<KafkaConsumerBuilderOptions<string, byte[]>> kafkaTopicOptionsMonitor,
        IMessageFactory messageFactory)
    {
        _loggerFactory = loggerFactory;
        _kafkaTopicOptionsMonitor = kafkaTopicOptionsMonitor;
        _messageFactory = messageFactory;
    }

    public IKafkaConsumer<TKey, TValue> Create<TKey, TValue>(
        Action<KafkaConsumerBuilderOptions<TKey, TValue>> setupAction)
    {
        var options = new KafkaConsumerBuilderOptions<TKey, TValue>();
        setupAction?.Invoke(options);

        return Create(options);
    }

    public IKafkaConsumer<TKey, TValue> Create<TKey, TValue>(KafkaConsumerBuilderOptions<TKey, TValue> options)
    {
        options.CreateTopicIfEnabled();

        return new KafkaConsumer<TKey, TValue>(options, _loggerFactory.CreateLogger<KafkaConsumer<TKey, byte[]>>(),
            _messageFactory);
    }

    // public IKafkaConsumer Create(KafkaConsumerBuilderOptions options)
    // {
    //     options.CreateTopicIfEnabled();
    //
    //     return new KafkaConsumer(options, _loggerFactory.CreateLogger<KafkaConsumer>(), _messageFactory);
    // }

    public IKafkaConsumer Get(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"'{nameof(name)}' must not be null, empty or whitespace.", nameof(name));
        }

        // This is being done outside of lock for optimistic locking.
        // ReSharper disable once InconsistentlySynchronizedField
        if (_instances.TryGetValue(name, out var instance))
        {
            return instance as IKafkaConsumer;
        }

        lock (_instancesLock)
        {
            if (_instances.TryGetValue(name, out var lockInstance))
            {
                return lockInstance as IKafkaConsumer;
            }

            var options = _kafkaTopicOptionsMonitor.Get(name);
            options.CreateTopicIfEnabled();

            var consumer = new KafkaConsumer(options, _loggerFactory.CreateLogger<KafkaConsumer>(), _messageFactory);
            _instances.Add(name, consumer);

            return consumer;
        }
    }

    public void Dispose()
    {
        // Dispose all registered consumers
        foreach (var instance in _instances.Values)
        {
            (instance as IDisposable)?.Dispose();
        }

        _loggerFactory?.Dispose();
    }
}
