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
    public TimeSpan TimeToLive { get; internal set; }
    
    /// <inheritdoc />
    public int SecondsRange { get; set; }
    
    /// <inheritdoc />
    public bool IsSliding { get; internal set; }
}