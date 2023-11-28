using System;
using SampleProject.Utils.Caching.Abstractions;

namespace SampleProject.Utils.Caching.Policy;

/// <summary>
/// Represents cache item settings for each providers.
/// </summary>
public class ItemPolicy
{
    /// <summary>
    /// The default ItemPolicy. Means the item is never cached.
    /// </summary>
    public ItemPolicy Default = new ItemPolicy { Name = "(default)" };

    /// <summary>
    /// Name of the Policy.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The local cache policy.
    /// </summary>
    public ICachePolicy Local { get; set; }

    /// <summary>
    /// A Fluent interface to create a new item policy.
    /// </summary>
    public static IItemPolicyBuilder ItemPolicyBuilder => new ItemPolicyBuilder();

    /// <summary>
    /// The Constructor.
    /// </summary>
    public ItemPolicy()
    {
        Name = "(notset)";
        Local = new CachePolicy();
    }

    /// <summary>
    /// Returns an instance of the <see cref="ItemPolicy"/> class with a local
    /// cache policy set to DoNotExpire.
    /// </summary>
    public static ItemPolicy DoNotExpire()
    {
        return ItemPolicyBuilder
            .LocalPolicy(CachePolicy.DoNotExpire)
            .Create("DoNotExpire");
    }


    /// <summary>
    /// Returns an instance of the <see cref="ItemPolicy"/> class with a local
    /// cache policy set to expire after one day.
    /// </summary>
    public static ItemPolicy ExpireAfterOneDay()
    {
        return ItemPolicyBuilder
            .LocalPolicy(TimeSpan.FromDays(1), false)
            .Create("ExpiryAfterOneDay");
    }

    /// <summary>
    /// Returns an instance of the <see cref="ItemPolicy"/> class with a local
    /// cache policy set to expire after one week.
    /// </summary>
    public static ItemPolicy ExpireAfterOneWeek()
    {
        return ItemPolicyBuilder
            .LocalPolicy(TimeSpan.FromDays(1), false)
            .Create("ExpiryAfterOneWeek");
    }

    /// <summary>
    /// Returns an instance of the <see cref="ItemPolicy"/> class with a local
    /// cache policy with sliding expiry after specified hours.
    /// </summary>
    public static ItemPolicy SlidingExpiryAfterHour(int hours)
    {
        return ItemPolicyBuilder
            .LocalPolicy(TimeSpan.FromHours(hours), true)
            .Create($"SlidingExpiry{hours}Hour");
    }
}