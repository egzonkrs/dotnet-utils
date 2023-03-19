using System.Collections.Generic;

namespace SampleProject.Tools.Messaging.Abstractions;

/// <summary>
/// Generic Message.
/// </summary>
/// <typeparam name="TKey">The message key type.</typeparam>
/// <typeparam name="TValue">The message value type.</typeparam>
public interface IMessage<TKey, TValue>
{
    /// <summary>
    /// The Key.
    /// </summary>
    public TKey Key { get; }

    /// <summary>
    /// The Value.
    /// </summary>
    public TValue Value { get; }

    /// <summary>
    /// The List of Headers.
    /// </summary>
    public IDictionary<string, string> Headers { get; }
}

/// <summary>
/// Generic Message.
/// </summary>
/// <typeparam name="TValue">The message value type.</typeparam>
public interface IMessage<TValue> : IMessage<string, TValue>
{
}