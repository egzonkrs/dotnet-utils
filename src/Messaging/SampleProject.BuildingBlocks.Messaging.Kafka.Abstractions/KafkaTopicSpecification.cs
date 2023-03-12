namespace SampleProject.BuildingBlocks.Messaging.Kafka.Abstractions;

/// <summary>
/// The Kafka Topic Specification
/// </summary>
public class KafkaTopicSpecification
{
    /// <summary>
    /// Name of the Topic.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Number of Partitions.
    /// </summary>
    public int Partitions { get; set; } = 1;

    /// <summary>
    /// Number of replicas of a topic in a Kafka cluster.
    /// </summary>
    public short ReplicationFactor { get; set; } = 1;

    /// <summary>
    /// Retention Period in Milliseconds.
    /// </summary>
    public long? RetentionPeriodInMs { get; set; }
}
