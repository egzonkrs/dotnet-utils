using System;
using JetBrains.Annotations;

namespace SampleProject.Tools.Http;

/// <summary>
/// The Http Client Options Builder.
/// </summary>
public class HttpClientOptionsBuilder
{
    /// <summary>
    /// The Base Address.
    /// </summary>
    public Uri BaseAddress { get; private set; }

    /// <summary>
    /// Sets the BaseAddress based on the Base Uri and Relative Uri.
    /// </summary>
    /// <param name="baseUri">The Base Uri.</param>
    /// <param name="relativeUri">The Relative Uri to append to the Base Uri.</param>
    /// <returns>The <see cref="HttpClientOptionsBuilder"/>.</returns>
    public HttpClientOptionsBuilder WithBaseAddress(string baseUri, [NotNull] string relativeUri)
    {
        if (string.IsNullOrWhiteSpace(relativeUri))
        {
            throw new ArgumentException($"'{nameof(relativeUri)}' must not be null, empty or whitespace.",
                nameof(relativeUri));
        }

        BaseAddress = string.IsNullOrWhiteSpace(baseUri)
            ? new Uri(relativeUri)
            : new Uri(new Uri(baseUri), relativeUri);

        return this;
    }

    /// <summary>
    /// Configures an <see cref="HttpClientOptionsBuilder"/> with Base Address.
    /// </summary>
    /// <param name="baseUri">The Base Uri.</param>
    /// <returns>The <see cref="HttpClientOptionsBuilder"/>.</returns>
    /// <exception cref="ArgumentNullException">Throws an <see cref="ArgumentNullException"/> when the <see cref="Uri"/> is not set.</exception>
    public HttpClientOptionsBuilder WithBaseAddress([NotNull] string baseUri)
    {
        if (string.IsNullOrWhiteSpace(baseUri))
        {
            throw new ArgumentException($"'{nameof(baseUri)}' must not be null, empty or whitespace.", 
                nameof(baseUri));
        }

        BaseAddress = new Uri(baseUri);

        return this;
    }

    /// <summary>
    /// Configures an <see cref="HttpClientOptionsBuilder"/> with Base Address.
    /// </summary>
    /// <param name="baseUri">The Base Uri.</param>
    /// <returns>The <see cref="HttpClientOptionsBuilder"/>.</returns>
    /// <exception cref="ArgumentNullException">Throws an <see cref="ArgumentNullException"/> when the <see cref="Uri"/> is not set.</exception>
    public HttpClientOptionsBuilder WithBaseAddress([NotNull] Uri baseUri)
    {
        BaseAddress = baseUri ?? throw new ArgumentNullException(nameof(baseUri));

        return this;
    }
}