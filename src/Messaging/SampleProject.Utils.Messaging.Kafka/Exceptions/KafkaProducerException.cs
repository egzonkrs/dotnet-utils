using System;

namespace SampleProject.Utils.Messaging.Kafka.Exceptions;

/// <inheritdoc />
public class KafkaProducerException : SystemException
{
    /// <inheritdoc />
    public KafkaProducerException(string message) : base(message)
    {
    }
}
