using System;
using System.Collections.Generic;
using System.Text;
using Confluent.Kafka;

namespace SampleProject.Tools.Messaging.Kafka.Extensions;

/// <summary>
/// Header extension methods.
/// </summary>
public static class HeadersExtensions
{
    private static readonly Encoding _encoding = Encoding.UTF8;

    /// <summary>
    /// Retrieves a Header by it's name.
    /// </summary>
    /// <param name="headers">The <see cref="Confluent.Kafka.Headers"/>.</param>
    /// <param name="headerName">The name of the header we are searching for.</param>
    /// <returns>A string that represents the Header object.</returns>
    public static string GetHeaderValue(this Headers headers, string headerName)
    {
        if (headers is null) throw new ArgumentNullException(nameof(headers));

        return headers.TryGetLastBytes(headerName, out var headerInBytes)
            ? _encoding.GetString(headerInBytes)
            : null;
    }

    /// <summary>
    /// Convert the <see cref="Confluent.Kafka.Headers"/> to a Dictionary of type <see cref="IDictionary{TKey,TValue}"/>.
    /// </summary>
    /// <param name="headers">The <see cref="Confluent.Kafka.Headers"/>.</param>
    /// <returns>The <see cref="IDictionary{TKey,TValue}"/></returns>
    public static IDictionary<string, string> ToDictionary(this Headers headers)
    {
        if (headers is null) throw new ArgumentNullException(nameof(headers));

        var dictionary = new Dictionary<string, string>();

        foreach (var header in headers)
        {
            var headerValue = _encoding.GetString(header.GetValueBytes());
            dictionary.Add(header.Key, headerValue);
        }

        return dictionary;
    }

    /// <summary>
    /// Add a header to the <see cref="Confluent.Kafka.Headers"/>.
    /// </summary>
    /// <param name="headers">The <see cref="Confluent.Kafka.Headers"/>.</param>
    /// <param name="key">The header key.</param>
    /// <param name="value">The header value.</param>
    public static void Add(this Headers headers, string key, string value)
    {
        if(headers is null) headers = new Headers();

        var byteValue = value is null ? null : _encoding.GetBytes(value);

        headers.Add(key, byteValue);
    }
}
