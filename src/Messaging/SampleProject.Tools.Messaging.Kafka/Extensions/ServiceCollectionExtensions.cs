using System;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SampleProject.Tools.Messaging.Kafka.Abstractions;
using SampleProject.Tools.Messaging.Kafka.Internal;

namespace SampleProject.Tools.Messaging.Kafka.Extensions;

/// <summary>
/// The <see cref="IServiceCollection"/> Extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the required services for a Kafka Producer.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <param name="configureAction">The <see><cref>Action{KafkaProducerBuilderOptions{TKey, TValue}}</cref></see> which will be used to set the Kafka Producer Builder Options.</param>
    /// <typeparam name="TKey">The Key of the Kafka Message.</typeparam>
    /// <typeparam name="TValue">The Value of the Kafka Message.</typeparam>
    /// <returns>The same <see cref="IServiceCollection"/> so that multiple calls can be chained.</returns>
    /// <exception cref="ArgumentNullException">Throws an <see cref="ArgumentNullException"/> when the <see cref="IServiceCollection"/> is not set.</exception>
    /// <exception cref="ArgumentNullException">Throws an <see cref="ArgumentNullException"/> when the <see><cref>Action{KafkaProducerBuilderOptions{TKey, TValue}}</cref></see> is not set.</exception>
    public static IServiceCollection AddKafkaProducer<TKey, TValue>([NotNull] this IServiceCollection services,
        [NotNull] Action<KafkaProducerBuilderOptions<TKey, TValue>> configureAction)
    {
        if (services is null) throw new ArgumentNullException(nameof(services));
        if (configureAction is null) throw new ArgumentNullException(nameof(configureAction));

        services.AddProducerInternal(configureAction);
        services.TryAddSingleton(x => x.GetRequiredService<IKafkaProducerFactory>().Create(configureAction));
        return services;
    }

    /// <summary>
    /// Adds the required services for a Kafka Producer.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <param name="configureAction">The <see><cref>Action{KafkaProducerBuilderOptions{string, TValue}}</cref></see> which will be used to set the Kafka Producer Builder Options.</param>
    /// <typeparam name="TValue">The Value of the Kafka Message.</typeparam>
    /// <returns>The same <see cref="IServiceCollection"/> so that multiple calls can be chained.</returns>
    /// <exception cref="ArgumentNullException">Throws an <see cref="ArgumentNullException"/> when the <see cref="IServiceCollection"/> is not set.</exception>
    /// <exception cref="ArgumentNullException">Throws an <see cref="ArgumentNullException"/> when the <see><cref>Action{KafkaProducerBuilderOptions{string, TValue}}</cref></see> is not set.</exception>
    public static IServiceCollection AddKafkaProducer<TValue>(
        [NotNull] this IServiceCollection services,
        [NotNull] Action<KafkaProducerBuilderOptions<string, TValue>> configureAction)
    {
        if (services is null) throw new ArgumentNullException(nameof(services));
        if (configureAction is null) throw new ArgumentNullException(nameof(configureAction));

        services.AddProducerInternal(configureAction);
        services.TryAddSingleton(x => x.GetRequiredService<IKafkaProducerFactory>().Create(configureAction));
        return services;
    }

    private static void AddProducerInternal<TKey, TValue>(this IServiceCollection services,
        Action<KafkaProducerBuilderOptions<TKey, TValue>> configureAction)
    {
        if (services is null) throw new ArgumentNullException(nameof(services));
        if (configureAction is null) throw new ArgumentNullException(nameof(configureAction));

        var options = new KafkaProducerBuilderOptions<TKey, TValue>();
        configureAction(options);
        options.ValidateAndThrow();

        services.Configure(configureAction);
        services.TryAddSingleton<IMessageFactory, MessageFactory>();
        services.TryAddSingleton<IKafkaProducerFactory, KafkaProducerFactory>();
    }
}
