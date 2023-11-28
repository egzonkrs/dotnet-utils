using JetBrains.Annotations;
using System.Threading.Tasks;

namespace SampleProject.Utils.Messaging.Kafka.Abstractions;

/// <summary>
/// Kafka Admin API.
/// </summary>
public interface IKafkaAdminClient
{
    /// <summary>
    /// Checks against the Kafka Cluster Metadata and automatically creates Kafka Topic.
    /// </summary>
    /// <param name="topicOptions">The Kafka Topic options.</param>
    Task CreateNonExistentTopicsAsync([NotNull] params KafkaTopicSpecification[] topicOptions);

    /// <summary>
    /// Creates Kafka Topic.
    /// </summary>
    /// <param name="topicOptions">The Kafka Topic options.</param>
    Task CreateTopicsAsync([NotNull] params KafkaTopicSpecification[] topicOptions);
}
