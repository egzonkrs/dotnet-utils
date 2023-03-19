using System;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace SampleProject.Tools.Messaging.Kafka.Abstractions;

/// <summary>
/// The Kafka Builder Options.
/// </summary>
public abstract class KafkaBuilderOptions
{
    internal KafkaOptions KafkaOptions { get; set; } = new KafkaOptions();
    internal KafkaTopicOptions KafkaTopicOptions { get; set; } = new KafkaTopicOptions();
    internal string Topic { get; private set; }

    /// <summary>
    /// Validates and Throws a <see cref="ConfigurationErrorsException"/> if Builder Options are not configured properly.
    /// </summary>
    /// <exception cref="ConfigurationErrorsException"></exception>
    public virtual void ValidateAndThrow()
    {
        if (string.IsNullOrWhiteSpace(Topic))
        {
            throw new ConfigurationErrorsException(
                $"{nameof(Topic)} is required. Make sure that you register the topic name via `WithTopic` method.");
        }

        if (string.IsNullOrWhiteSpace(KafkaOptions.Brokers))
        {
            throw new ConfigurationErrorsException("Brokers must not be null, empty or whitespace!");
        }
    }

    /// <summary>
    /// Loads the <see cref="KafkaOptions"/> from Configuration.
    /// </summary>
    /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
    protected void FromConfiguration(IConfiguration configuration)
    {
        if (configuration is null) throw new ArgumentNullException(nameof(configuration));

        var configurationSection = configuration.GetSection(KafkaOptions.DefaultSectionName);
        FromConfiguration(configurationSection);
    }

    /// <summary>
    /// Loads the <see cref="KafkaOptions"/> from Configuration.
    /// </summary>
    /// <param name="configurationSection">The <see cref="IConfigurationSection"/>.</param>
    protected void FromConfiguration(IConfigurationSection configurationSection)
    {
        if (configurationSection is null) throw new ArgumentNullException(nameof(configurationSection));

        KafkaOptions = configurationSection?.Get<KafkaOptions>() ?? new KafkaOptions();
    }

    /// <summary>
    /// Registers a Kafka Topic.
    /// </summary>
    /// <param name="topicName"></param>
    /// <exception cref="ArgumentException"></exception>
    protected void WithTopic(string topicName)
    {
        if (string.IsNullOrWhiteSpace(topicName))
        {
            throw new ArgumentException($"'{nameof(topicName)}' must not be null, empty or whitespace!",
                nameof(topicName));
        }

        Topic = topicName;
    }

    /// <summary>
    /// Configures Topic creation from Configuration.
    /// </summary>
    /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
    protected void ConfigureTopicCreation(IConfiguration configuration)
    {
        if (configuration is null) throw new ArgumentNullException(nameof(configuration));

        ConfigureTopicCreation(configuration.GetSection(KafkaTopicOptions.DefaultSectionName));
    }

    /// <summary>
    /// Configures Topic creation from Configuration.
    /// </summary>
    /// <param name="configurationSection">The <see cref="IConfigurationSection"/>.</param>
    protected void ConfigureTopicCreation(IConfigurationSection configurationSection)
    {
        if (configurationSection is null) throw new ArgumentNullException(nameof(configurationSection));

        KafkaTopicOptions = configurationSection?.Get<KafkaTopicOptions>() ?? new KafkaTopicOptions();
    }
}