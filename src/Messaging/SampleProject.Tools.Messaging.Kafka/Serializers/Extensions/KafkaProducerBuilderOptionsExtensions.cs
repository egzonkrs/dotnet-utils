using SampleProject.Tools.Messaging.Kafka.Abstractions;

namespace SampleProject.Tools.Messaging.Kafka.Serializers.Extensions;

/// <summary>
/// The <see cref="KafkaProducerBuilderOptions{TKey,TValue}"/> extensions.
/// </summary>
public static class KafkaProducerBuilderOptionsExtensions
{
    /// <summary>
    /// Configures a Json Kafka Serializer for Value Serialization.
    /// </summary>
    /// <param name="options">The <see cref="KafkaProducerBuilderOptions{TKey,TValue}"/>.</param>
    /// <typeparam name="TKey">The Key of the Kafka Message.</typeparam>
    /// <typeparam name="TValue">The Value of the Kafka Message.</typeparam>
    /// <returns>The <see cref="KafkaProducerBuilderOptions{TKey,TValue}"/>.</returns>
    public static KafkaProducerBuilderOptions<TKey, TValue> WithJson<TKey, TValue>(
        this KafkaProducerBuilderOptions<TKey, TValue> options)
    {
        options.ValueSerializer = new JsonKafkaSerializer();

        return options;
    }
}
