using System;
using System.Collections.Generic;
using System.Net.Http;
using JetBrains.Annotations;

namespace SampleProject.Tools.Http;

/// <summary>
/// Http Client Builder. 
/// </summary>
public class HttpClientBuilder
{
    private readonly List<DelegatingHandler> _delegatingHandlers = new();

    internal DelegatingHandler[] DelegatingHandlers => _delegatingHandlers.ToArray();
    
    internal HttpMessageHandler MessageHandler { get; private set; } = new HttpClientHandler();

    /// <summary>
    /// The Base Address.
    /// </summary>
    public HttpClientOptionsBuilder Options { get; } = new HttpClientOptionsBuilder();

    /// <summary>
    /// Adds a Delegating Handler.
    /// </summary>
    /// <param name="handler">The <see cref="DelegatingHandler"/>.</param>
    /// <returns>The <see cref="HttpClientBuilder"/>.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public HttpClientBuilder AddDelegatingHandler([NotNull] DelegatingHandler handler)
    {
        if (handler is null) throw new ArgumentNullException(nameof(handler));
        
        _delegatingHandlers.Add(handler);

        return this;
    }
    
    /// <summary>
    /// Adds HTTP Message Handler.
    /// </summary>
    /// <param name="handler">The <see cref="HttpMessageHandler"/>.</param>
    /// <returns>The <see cref="HttpClientBuilder"/>.</returns>
    public HttpClientBuilder WithMessageHandler([NotNull] HttpMessageHandler handler)
    {
        MessageHandler = handler ?? throw new ArgumentNullException(nameof(handler));
            
        return this;
    }
}