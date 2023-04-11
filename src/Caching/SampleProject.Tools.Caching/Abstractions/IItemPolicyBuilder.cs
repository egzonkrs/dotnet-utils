using System;
using SampleProject.Tools.Caching.Policy;

namespace SampleProject.Tools.Caching.Abstractions;

/// <summary>
/// Fluent ItemPolicy configurator. Required to create cache item policies.
/// </summary>
public interface IItemPolicyBuilder
{
    /// <summary>
    /// Configuration settings for the local cache.
    /// </summary>
    /// <param name="cachePolicy">The <see cref="ICachePolicy"/>.</param>
    /// <returns>The same <see cref="IItemPolicyBuilder"/> so that multiple calls can be chained.</returns>
    IItemPolicyBuilder LocalPolicy(ICachePolicy cachePolicy);
    
    /// <summary>
    /// Configuration settings for the local cache with TTL (seconds) specified.
    /// </summary>
    /// <param name="timeToLiveInSeconds">Time to Live in seconds.</param>
    /// <returns>The same <see cref="IItemPolicyBuilder"/> so that multiple calls can be chained.</returns>
    IItemPolicyBuilder LocalPolicy(int timeToLiveInSeconds);
    

    /// <summary>
    /// Configuration settings for the local cache with TTL (seconds) and IsSliding specified.
    /// </summary>
    /// <param name="timeToLiveInSeconds">TTL in seconds.</param>
    /// <param name="isSliding">The <see cref="ICachePolicy.IsSliding"/>.</param>
    /// <returns>The same <see cref="IItemPolicyBuilder"/> so that multiple calls can be chained.</returns>
    IItemPolicyBuilder LocalPolicy(int timeToLiveInSeconds,  bool isSliding);
    
    /// <summary>
    /// Configuration settings for the local cache with TTL (seconds) and random seconds specified.
    /// </summary>
    /// <param name="timeToLiveInSeconds">TTL in seconds</param>
    /// <param name="maxRandomSeconds">Indicates the amount of seconds of the range to use when adding a
    /// random number of seconds to the request.</param>
    /// <returns>The same <see cref="IItemPolicyBuilder"/> so that multiple calls can be chained.</returns>
    IItemPolicyBuilder LocalPolicy(int timeToLiveInSeconds, int maxRandomSeconds);
    
    /// <summary>
    /// Configuration settings for the local cache with TTL specified.
    /// </summary>
    /// <param name="timeToLive">Time to Live in time interval.</param>
    /// <returns>The same <see cref="IItemPolicyBuilder"/> so that multiple calls can be chained.</returns>
    IItemPolicyBuilder LocalPolicy(TimeSpan timeToLive);
    

    /// <summary>
    /// Configuration settings for the local cache with TTL and IsSliding specified.
    /// </summary>
    /// <param name="timeToLive">Time to Live in time interval.</param>
    /// <param name="isSliding">The <see cref="ICachePolicy.IsSliding"/>.</param>
    /// <returns>The same <see cref="IItemPolicyBuilder"/> so that multiple calls can be chained.</returns>
    IItemPolicyBuilder LocalPolicy(TimeSpan timeToLive,  bool isSliding);
    
    /// <summary>
    /// Configuration settings for the local cache with TTL and random seconds specified.
    /// </summary>
    /// <param name="timeToLive">TTL in time interval.</param>
    /// <param name="maxRandomSeconds">Indicates the amount of seconds of the range to use when adding a
    /// random number of seconds to the request.</param>
    /// <returns>The same <see cref="IItemPolicyBuilder"/> so that multiple calls can be chained.</returns>
    IItemPolicyBuilder LocalPolicy(TimeSpan timeToLive, int maxRandomSeconds);
    
    /// <summary>
    /// Creates the policy based on the provided configuration.
    /// </summary>
    /// <returns>An <see cref="ItemPolicy"/> instance.</returns>
    ItemPolicy Create(string name);
}