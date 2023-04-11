using System;
using SampleProject.Tools.Caching.Abstractions;

namespace SampleProject.Tools.Caching.Policy;

/// <inheritdoc />
public class ItemPolicyBuilder : IItemPolicyBuilder
{
    /// <inheritdoc />
    public IItemPolicyBuilder LocalPolicy(ICachePolicy cachePolicy)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IItemPolicyBuilder LocalPolicy(int timeToLiveInSeconds)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IItemPolicyBuilder LocalPolicy(int timeToLiveInSeconds, bool isSliding)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IItemPolicyBuilder LocalPolicy(TimeSpan timeToLive, int maxRandomSeconds)
    {
        throw new NotImplementedException();
    }
}