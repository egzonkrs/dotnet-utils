using System;
using SampleProject.Tools.Caching.Abstractions;

namespace SampleProject.Tools.Caching.Policy;

/// <inheritdoc />
public class CachePolicy : ICachePolicy
{
    /// <summary>
    /// Default CachePolicy with no expiry.
    /// </summary>
    public static readonly CachePolicy DoNotExpire = new CachePolicy();
    
    /// <inheritdoc />
    public TimeSpan TimeToLive { get; }
    
    /// <inheritdoc />
    public int SecondsRange { get; }
    
    /// <inheritdoc />
    public bool IsSliding { get; }
}