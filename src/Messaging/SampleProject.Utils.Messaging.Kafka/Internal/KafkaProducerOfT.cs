using System;
using Polly;
using Confluent.Kafka;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SampleProject.Utils.Messaging.Kafka.Abstractions;
using SampleProject.Utils.Messaging.Kafka.Exceptions;

namespace SampleProject.Utils.Messaging.Kafka.Internal;

internal class KafkaProducer<TKey, TValue> : IKafkaProducer<TKey, TValue>
{
    private readonly IAsyncPolicy _policyHandler;
    private readonly IKafkaSerializer _serializer;
    private readonly IMessageFactory _messageFactory;
    private readonly IProducer<TKey, byte[]> _producer;
    private readonly ILogger<KafkaProducer<TKey, TValue>> _logger;

    private bool _isDisposing;
    private readonly string _topic;

    internal KafkaProducer(
        KafkaProducerBuilderOptions<TKey, TValue> options,
        ILogger<KafkaProducer<TKey, TValue>> logger,
        IMessageFactory messageFactory)
    {
        options.ValidateAndThrow();

        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _policyHandler = options.PolicyHandler;
        _messageFactory = messageFactory;

        _producer = new ProducerBuilder<TKey, byte[]>(options.KafkaOptions.ToProducerProperties())
            .SetLogHandler((_, e) => logger.LogInformation(e.Message))
            .SetErrorHandler((_, e) =>
            {
                logger.LogError(e.Reason);
            })
            .Build();

        _topic = options.Topic;
        _serializer = options.ValueSerializer;
    }

    public async Task ProduceAsync(IKafkaMessage<TKey, TValue> kafkaMessage, CancellationToken cancellationToken = default)
    {
        var message = _messageFactory.SerializeMessage(_serializer, kafkaMessage);

        await _policyHandler.ExecuteAsync(() => ProduceAsync(message, cancellationToken));
    }

    private async Task ProduceAsync(Message<TKey, byte[]> message, CancellationToken cancellationToken)
    {
        try
        {
            var deliveryReport = await _producer.ProduceAsync(_topic, message, cancellationToken);

            _logger.LogDebug(
                "Delivered 'key: {key}' - '{value}' to '{topicPartitionOffset}' with offset '{offset}'",
                deliveryReport.Key, deliveryReport.Value, deliveryReport.TopicPartitionOffset,
                deliveryReport.Offset);
        }
        catch (ProduceException<TKey, TValue> ex)
        {
            _logger.LogError(ex, "Delivery failed: {reason}", ex.Error.Reason);
            throw new KafkaProducerException(ex.Message);
        }
    }

    public void Dispose()
    {
        if(_isDisposing) return;

        _isDisposing = true;

        _logger.LogDebug("Disposing Kafka Producer [{producerName}]...", _producer.Name);

        _producer.Flush(TimeSpan.FromSeconds(5));
        _producer?.Dispose();
    }
}
