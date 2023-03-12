namespace SampleProject.BuildingBlocks.Messaging.Kafka.Abstractions;

/// <summary>
/// The Kafka Topic Options.
/// </summary>
public class KafkaTopicOptions
{
    /// <summary>
    /// The configuration default section name.
    /// </summary>
    public const string DefaultSectionName = KafkaOptions.DefaultSectionName + ":Topic";

    /// <summary>
    /// If enabled, topics will be created on application startup.
    /// </summary>
    public bool IsTopicCreationEnabled { get; set; } = false;

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
