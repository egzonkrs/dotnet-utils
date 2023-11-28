using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using SampleProject.Utils.Messaging.Kafka.Abstractions;
using SampleProject.Utils.Messaging.Kafka.Clients;

namespace SampleProject.Utils.Messaging.Kafka.Internal;

internal static class KafkaBuilderOptionsExtensions
{
    internal static void CreateTopicIfEnabled([NotNull] this KafkaBuilderOptions builderOptions)
    {
        if (builderOptions is null) throw new ArgumentNullException(nameof(builderOptions));

        if (builderOptions.KafkaTopicOptions.IsTopicCreationEnabled)
        {
            Task.Run(async () =>
            {
                using var client = new KafkaAdminClient(builderOptions.KafkaOptions);
                await client.CreateNonExistentTopicsAsync(new KafkaTopicSpecification
                {
                    Name = builderOptions.Topic,
                    Partitions = builderOptions.KafkaTopicOptions.Partitions,
                    ReplicationFactor = builderOptions.KafkaTopicOptions.ReplicationFactor,
                    RetentionPeriodInMs = builderOptions.KafkaTopicOptions.RetentionPeriodInMs
                });
            }).Wait();
        }
    }
}
