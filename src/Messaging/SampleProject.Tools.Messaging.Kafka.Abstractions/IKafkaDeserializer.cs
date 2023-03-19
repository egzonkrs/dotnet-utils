namespace SampleProject.Tools.Messaging.Kafka.Abstractions;

/// <summary>
/// The Kafka Deserializer.
/// </summary>
public interface IKafkaDeserializer
{
    /// <summary>
    /// Deserializes a Byte Array to a Type.
    /// </summary>
    TValue Deserialize<TValue>(byte[] data);
}
