namespace SampleProject.BuildingBlocks.Messaging.Kafka.Abstractions;

/// <summary>
/// The Kafka Serializer.
/// </summary>
public interface IKafkaSerializer
{
    /// <summary>
    /// Serializes data to a Byte Array.
    /// </summary>
    byte[] Serialize<T>(T data);
}
