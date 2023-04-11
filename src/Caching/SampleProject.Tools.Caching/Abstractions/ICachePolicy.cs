using System;

namespace SampleProject.Tools.Caching.Abstractions;

/// <summary>
/// Defines Cache Item Settings.
/// </summary>
public interface ICachePolicy
{
    /// <summary>
    /// Represents the time-to-live (TTL) of a cached item, which is the duration
    /// for which the item should remain in the cache before it expires and is removed.
    /// </summary>
    TimeSpan TimeToLive { get; }

    /// <summary>
    /// When you set a time-to-live (TTL) value for a cache item, it means that the item will be stored
    /// in the cache for a specific duration before it is evicted. For example, you might set a TTL
    /// of 1 hour for a frequently accessed item, which means that the item will be evicted from the
    /// cache after 1 hour of being stored. However, if you have many such frequently accessed items with
    /// the same TTL value, they might all expire at the same time, leading to a sudden burst of cache misses
    /// and putting extra load on your system as the data is reloaded into the cache.
    /// To avoid this situation, some caching providers allow you to add a random offset to the TTL of each
    /// cache item. This means that the actual expiration time of the item will be the TTL plus a random number
    /// of seconds within a specified range. For example, let's say you have set a TTL of 1 hour for a
    /// cache item, and the SecondsRange property is set to 300 seconds (5 minutes). This means that the
    /// actual expiration time of the item will be between 1 hour and 1 hour 5 minutes. The exact expiration
    /// time will be chosen randomly within this range, which will spread out the expiration times of the
    /// cache items, preventing a sudden burst of cache misses. So, in short, the SecondsRange property
    /// allows you to add a random offset to the TTL of a cache item, which helps prevent sudden bursts
    /// of cache misses by spreading out the expiration times of the items.
    /// </summary>
    int SecondsRange { get; }
    
    /// <summary>
    /// Determines whether a cached item should be expired using sliding expiration or absolute expiration.
    /// When IsSliding is set to true, sliding expiration is used, which extends the expiration time of the
    /// item every time it is accessed. When IsSliding is set to false, absolute expiration is used, and the
    /// item is evicted from the cache after the TTL duration, regardless of whether it has been accessed or not.
    /// </summary>
    bool IsSliding { get; }
}