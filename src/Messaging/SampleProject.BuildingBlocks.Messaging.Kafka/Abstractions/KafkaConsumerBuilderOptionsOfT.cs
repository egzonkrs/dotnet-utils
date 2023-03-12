using System.Configuration;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;

namespace SampleProject.BuildingBlocks.Messaging.Kafka.Abstractions;

/// <summary>
/// The Kafka Consumer Builder Options.
/// </summary>
/// <typeparam name="TKey">The Kafka Message Key.</typeparam>
/// <typeparam name="TValue">The Kafka Message Value.</typeparam>
public class KafkaConsumerBuilderOptions<TKey, TValue> : KafkaBuilderOptions
{
    /// <summary>
    /// The <see cref="IKafkaDeserializer"/> used for Value Deserialization.
    /// </summary>
    public IKafkaDeserializer ValueDeserializer { internal get; set; }

    /// <summary>
    /// Loads the <see cref="KafkaOptions"/> from Configuration.
    /// </summary>
    /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
    /// <returns></returns>
    public new KafkaConsumerBuilderOptions<TKey, TValue> FromConfiguration([NotNull] IConfiguration configuration)
    {
        base.FromConfiguration(configuration);

        return this;
    }

    /// <summary>
    /// Loads the <see cref="KafkaOptions"/> from Configuration.
    /// </summary>
    /// <param name="configurationSection">The <see cref="IConfigurationSection"/>.</param>
    /// <returns></returns>
    public new KafkaConsumerBuilderOptions<TKey, TValue> FromConfiguration([NotNull] IConfigurationSection configurationSection)
    {
        base.FromConfiguration(configurationSection);

        return this;
    }

    /// <summary>
    /// Registers a Kafka Topic.
    /// </summary>
    /// <param name="topicName">The Topic Name.</param>
    /// <returns></returns>
    public new KafkaConsumerBuilderOptions<TKey, TValue> WithTopic([NotNull] string topicName)
    {
        base.WithTopic(topicName);

        return this;
    }

    /// <summary>
    /// Configures Topic creation from Configuration.
    /// </summary>
    /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
    /// <returns></returns>
    public new KafkaConsumerBuilderOptions<TKey, TValue> ConfigureTopicCreation([NotNull] IConfiguration configuration)
    {
        base.ConfigureTopicCreation(configuration);

        return this;
    }

    /// <summary>
    /// Configures Topic creation from Configuration.
    /// </summary>
    /// <param name="configurationSection">The <see cref="IConfigurationSection"/>.</param>
    /// <returns></returns>
    public new KafkaConsumerBuilderOptions<TKey, TValue> ConfigureTopicCreation([NotNull] IConfigurationSection configurationSection)
    {
        base.ConfigureTopicCreation(configurationSection);

        return this;
    }

    /// <inheritdoc />
    public override void ValidateAndThrow()
    {
        base.ValidateAndThrow();

        if (ValueDeserializer is null && typeof(TValue) != typeof(byte[]))
        {
            throw new ConfigurationErrorsException(
                $"{nameof(ValueDeserializer)} is required. Make sure that you register the serializer via `With*` method.");
        }
    }
}
