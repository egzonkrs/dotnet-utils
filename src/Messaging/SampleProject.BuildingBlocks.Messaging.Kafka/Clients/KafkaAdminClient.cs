using System;
using System.Linq;
using Confluent.Kafka;
using Confluent.Kafka.Admin;
using JetBrains.Annotations;
using System.Threading.Tasks;
using System.Collections.Generic;
using SampleProject.BuildingBlocks.Messaging.Kafka.Abstractions;

namespace SampleProject.BuildingBlocks.Messaging.Kafka.Clients;

/// <inheritdoc cref="IKafkaAdminClient"/>
public class KafkaAdminClient : IKafkaAdminClient, IDisposable
{
    private readonly IAdminClient _adminClient;

    /// <summary>
    /// The Constructor.
    /// </summary>
    /// <param name="kafkaOptions">The <see cref="KafkaOptions"/></param>
    public KafkaAdminClient([NotNull] KafkaOptions kafkaOptions)
    {
        if (kafkaOptions is null) throw new ArgumentNullException(nameof(kafkaOptions));

        _adminClient = new AdminClientBuilder(kafkaOptions.ToAdminProperties()).Build();
    }

    /// <inheritdoc />
    public async Task CreateNonExistentTopicsAsync(params KafkaTopicSpecification[] topicOptions)
    {
        if (topicOptions is null) throw new ArgumentNullException(nameof(topicOptions));
        if (!topicOptions.Any()) return;

        var metadata = GetMetaData();
        var topicsToCreate = topicOptions.Where(x => !metadata.Topics.Exists(m => m.Topic == x.Name));

        var kafkaTopicSpecifications = topicsToCreate as KafkaTopicSpecification[] ?? topicsToCreate.ToArray();

        if (kafkaTopicSpecifications.Any())
        {
            await CreateTopicsAsync(kafkaTopicSpecifications);
        }
    }

    /// <inheritdoc />
    public async Task CreateTopicsAsync(params KafkaTopicSpecification[] topicOptions)
    {
        if (topicOptions is null) throw new ArgumentNullException(nameof(topicOptions));
        if (!topicOptions.Any()) return;

        await _adminClient.CreateTopicsAsync(topicOptions.Select(x =>
        {
            var configs = new Dictionary<string, string>();

            if (x.RetentionPeriodInMs.HasValue)
            {
                configs.Add("retention.ms", x.RetentionPeriodInMs.ToString());
            }

            return new TopicSpecification
            {
                Name = x.Name,
                NumPartitions = x.Partitions,
                ReplicationFactor = x.ReplicationFactor,
                Configs = configs
            };
        }));
    }

    /// <summary>
    /// Used to get the Kafka Cluster Metadata.
    /// </summary>
    public Metadata GetMetaData()
    {
        return _adminClient.GetMetadata(TimeSpan.FromSeconds(30));
    }

    /// <inheritdoc />
    public void Dispose()
    {
        _adminClient?.Dispose();
    }
}
