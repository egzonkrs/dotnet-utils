using System;

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
}