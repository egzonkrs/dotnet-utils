using System;
using SampleProject.Tools.Caching.Abstractions;

namespace SampleProject.Tools.Caching.Policy;

/// <inheritdoc />
public class ItemPolicyBuilder : IItemPolicyBuilder
{
    private readonly ItemPolicy _itemPolicy;

    /// <summary>
    /// The Constructor.
    /// </summary>
    public ItemPolicyBuilder()
    {
        _itemPolicy = new ItemPolicy();
    }
    
    // public IItemPolicyBuilder LocalPolicy(TimeSpan timeToLive, int maxRandomSeconds, bool isSliding)
    // {
    //     _itemPolicy.Local = new CachePolicy
    //     {
    //         TimeToLive = timeToLive,
    //         SecondsRange = maxRandomSeconds,
    //         IsSliding = isSliding
    //     };
    //     
    //     return this;
    // }


    /// <inheritdoc />
    public IItemPolicyBuilder LocalPolicy(ICachePolicy cachePolicy)
    {
        _itemPolicy.Local = cachePolicy;

        return this;
    }

    /// <inheritdoc />
    public IItemPolicyBuilder LocalPolicy(int timeToLiveInSeconds)
    {
        _itemPolicy.Local = new CachePolicy
        {
            TimeToLive = TimeSpan.FromSeconds(timeToLiveInSeconds),
            SecondsRange = 0,
            IsSliding = false
        };

        return this;
    }

    /// <inheritdoc />
    public IItemPolicyBuilder LocalPolicy(int timeToLiveInSeconds, bool isSliding)
    {
        _itemPolicy.Local = new CachePolicy
        {
            TimeToLive = TimeSpan.FromSeconds(timeToLiveInSeconds),
            SecondsRange = 0,
            IsSliding = isSliding
        };

        return this;
    }

    /// <inheritdoc />
    public IItemPolicyBuilder LocalPolicy(int timeToLiveInSeconds, int maxRandomSeconds)
    {
        _itemPolicy.Local = new CachePolicy
        {
            TimeToLive = TimeSpan.FromSeconds(timeToLiveInSeconds),
            SecondsRange = maxRandomSeconds,
            IsSliding = false
        };

        return this;
    }

    /// <inheritdoc />
    public IItemPolicyBuilder LocalPolicy(TimeSpan timeToLive)
    {
        _itemPolicy.Local = new CachePolicy
        {
            TimeToLive = timeToLive,
            SecondsRange = 0,
            IsSliding = false
        };

        return this;
    }

    /// <inheritdoc />
    public IItemPolicyBuilder LocalPolicy(TimeSpan timeToLive, bool isSliding)
    {
        _itemPolicy.Local = new CachePolicy
        {
            TimeToLive = timeToLive,
            SecondsRange = 0,
            IsSliding = isSliding
        };

        return this;
    }

    /// <inheritdoc />
    public IItemPolicyBuilder LocalPolicy(TimeSpan timeToLive, int maxRandomSeconds)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public ItemPolicy Create(string name)
    {
        throw new NotImplementedException();
    }
}