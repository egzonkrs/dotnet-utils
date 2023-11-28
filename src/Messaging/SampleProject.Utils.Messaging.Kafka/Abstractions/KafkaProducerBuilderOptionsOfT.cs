using System;
using System.Configuration;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Polly;

namespace SampleProject.Utils.Messaging.Kafka.Abstractions;

/// <summary>
/// The Kafka Producer Builder Options.
/// </summary>
/// <typeparam name="TKey">The Kafka Message Type.</typeparam>
/// <typeparam name="TValue">The Kafka Message Value.</typeparam>
public class KafkaProducerBuilderOptions<TKey, TValue> : KafkaBuilderOptions
{
    /// <summary>
    /// The <see cref="IKafkaSerializer"/> used for Value Deserialization.
    /// </summary>
    public IKafkaSerializer ValueSerializer { internal get; set; }

    internal IAsyncPolicy PolicyHandler { get; set; } = Policy.NoOpAsync();

    /// <summary>
    /// Loads the <see cref="KafkaOptions"/> from Configuration.
    /// </summary>
    /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
    /// <returns></returns>
    public new KafkaProducerBuilderOptions<TKey, TValue> FromConfiguration([NotNull] IConfiguration configuration)
    {
        base.FromConfiguration(configuration);

        return this;
    }

    /// <summary>
    /// Configures a Policy Handler.
    /// </summary>
    /// <param name="policyHandler">The <see cref="IAsyncPolicy"/>.</param>
    /// <returns></returns>
    public KafkaProducerBuilderOptions<TKey, TValue> FromConfiguration([NotNull] IAsyncPolicy policyHandler)
    {
        PolicyHandler = policyHandler ?? throw new ArgumentNullException(nameof(policyHandler));

        return this;
    }

    /// <summary>
    /// Registers a Kafka Topic.
    /// </summary>
    /// <param name="topicName">The Topic Name.</param>
    /// <returns></returns>
    public new KafkaProducerBuilderOptions<TKey, TValue> WithTopic([NotNull] string topicName)
    {
        base.WithTopic(topicName);

        return this;
    }

    /// <summary>
    /// Configures Topic creation from Configuration.
    /// </summary>
    /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
    /// <returns></returns>
    public new KafkaProducerBuilderOptions<TKey, TValue> ConfigureTopicCreation([NotNull] IConfiguration configuration)
    {
        base.ConfigureTopicCreation(configuration);

        return this;
    }

    /// <summary>
    /// Configures Topic creation from Configuration.
    /// </summary>
    /// <param name="configurationSection">The <see cref="IConfigurationSection"/>.</param>
    /// <returns></returns>
    public new KafkaProducerBuilderOptions<TKey, TValue> ConfigureTopicCreation([NotNull] IConfigurationSection configurationSection)
    {
        base.ConfigureTopicCreation(configurationSection);

        return this;
    }

    /// <inheritdoc />
    public override void ValidateAndThrow()
    {
        base.ValidateAndThrow();

        if (ValueSerializer is null)
        {
            throw new ConfigurationErrorsException(
                $"{nameof(ValueSerializer)} is required. Make sure that you register the serializer via `With*` method.");
        }
    }
}
