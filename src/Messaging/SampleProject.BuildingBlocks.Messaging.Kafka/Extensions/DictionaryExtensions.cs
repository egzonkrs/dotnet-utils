using System;
using System.Collections.Generic;
using Confluent.Kafka;
using JetBrains.Annotations;

namespace SampleProject.BuildingBlocks.Messaging.Kafka.Extensions;

/// <summary>
/// Dictionary extension methods.
/// </summary>
public static class DictionaryExtensions
{
    /// <summary>
    /// Add or Update a Dictionary item.
    /// </summary>
    /// <param name="dictionary">The <see cref="IDictionary{TKey,TValue}"/>.</param>
    /// <param name="key">The key of the dictionary item to add.</param>
    /// <param name="value">The value of the dictionary item to add.</param>
    public static void AddOrUpdate<TKey, TValue>([NotNull] this IDictionary<TKey, TValue> dictionary, [NotNull] TKey key, [NotNull] TValue value)
    {
        if (dictionary is null) throw new ArgumentNullException(nameof(dictionary));
        if (key is null) throw new ArgumentNullException(nameof(key));
        if (key is string && string.IsNullOrWhiteSpace(key.ToString()))
            throw new ArgumentException($"Missing {nameof(key)}.", nameof(key));

        if (dictionary.ContainsKey(key))
        {
            dictionary[key] = value;
        }
        else
        {
            dictionary.Add(key, value);
        }
    }

    /// <summary>
    /// Convert the <see cref="T:IDictionary{string, string}"/> to a <see cref="Confluent.Kafka.Headers"/> object.
    /// </summary>
    /// <param name="dictionary">The <see cref="T:IDictinoary{string, string}"/>.</param>
    /// <returns>The <see cref="Confluent.Kafka.Headers"/>.</returns>
    public static Headers ToKafkaHeaders(this IDictionary<string, string> dictionary)
    {
        var headers = new Headers();

        if (dictionary is null) return headers;

        foreach (var keyValuePair in dictionary)
        {
            headers.Add(keyValuePair.Key, keyValuePair.Value);
        }

        return headers;
    }
}
