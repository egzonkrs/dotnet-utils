using System;

namespace SampleProject.BuildingBlocks.Messaging.Kafka.Exceptions;

/// <inheritdoc />
public class KafkaProducerException : SystemException
{
    /// <inheritdoc />
    public KafkaProducerException(string message) : base(message)
    {
    }
}
