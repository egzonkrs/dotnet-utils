using System;

namespace SampleProject.Tools.Messaging.Abstractions;

/// <summary>
/// Generic Error Message Context.
/// </summary>
/// <typeparam name="T">The message type.</typeparam>
public interface IErrorMessageContext<T>
{
    /// <summary>
    /// The Message.
    /// </summary>
    IMessageContext<T> Context { get; }

    /// <summary>
    /// The Raw payload in Bytes.
    /// </summary>
    byte[] RawValue { get; }

    /// <summary>
    /// The Error Type.
    /// </summary>
    ErrorType ErrorType { get; }

    /// <summary>
    /// The Exception.
    /// </summary>
    Exception InnerException { get; }
}