using System;

namespace SampleProject.BuildingBlocks.Messaging.Kafka.Abstractions;

/// <summary>
/// Provides a Factory to create an instance of <see cref="IKafkaProducer{TKey, TValue}"/>.
/// </summary>
public interface IKafkaProducerFactory
{
    /// <summary>
    /// Creates a Kafka Producer.
    /// </summary>
    /// <param name="setupAction">
    /// The <see><cref>Action{KafkaProducerBuilderOptions{TKey, TValue}}</cref></see>
    /// which will be used to set the <see cref="KafkaConsumerBuilderOptions{TKey, TValue}"/>
    /// </param>
    /// <typeparam name="TKey">The Key of the Kafka Message.</typeparam>
    /// <typeparam name="TValue">The Value of the Kafka Message.</typeparam>
    /// <returns>The <see cref="IKafkaProducer{TKey,TValue}"/>.</returns>
    IKafkaProducer<TKey, TValue> Create<TKey, TValue>(Action<KafkaProducerBuilderOptions<TKey, TValue>> setupAction);

    /// <summary>
    /// Creates a Kafka Producer.
    /// </summary>
    /// <param name="setupAction">
    /// The <see><cref>Action{KafkaProducerBuilderOptions{TKey, TValue}}</cref></see>
    /// which will be used to set the <see cref="KafkaConsumerBuilderOptions{TKey, TValue}"/>
    /// </param>
    /// <typeparam name="TValue">The Value of the Kafka Message.</typeparam>
    /// <returns>The <see cref="IKafkaProducer{TKey,TValue}"/>.</returns>
    IKafkaProducer<TValue> Create<TValue>(Action<KafkaProducerBuilderOptions<string, TValue>> setupAction);
}
