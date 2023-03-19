using System;

namespace SampleProject.Tools.Messaging.Kafka.Exceptions;

/// <inheritdoc />
public class KafkaProducerException : SystemException
{
    /// <inheritdoc />
    public KafkaProducerException(string message) : base(message)
    {
    }
}
