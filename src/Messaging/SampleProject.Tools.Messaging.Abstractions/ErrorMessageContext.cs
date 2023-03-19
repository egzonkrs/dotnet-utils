using System;

namespace SampleProject.Tools.Messaging.Abstractions;

/// <inheritdoc />
public class ErrorMessageContext<T> : IErrorMessageContext<T>
{
    /// <inheritdoc />
    public IMessageContext<T> Context { get; }

    /// <inheritdoc />
    public byte[] RawValue { get; }

    /// <inheritdoc />
    public ErrorType ErrorType { get; }

    /// <inheritdoc />
    public Exception InnerException { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    public ErrorMessageContext(ErrorType errorType, Exception ex, byte[] rawValue)
    {
        RawValue = rawValue;
        InnerException = ex;
        ErrorType = errorType;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public ErrorMessageContext(ErrorType errorType, Exception ex, byte[] rawValue, IMessageContext<T> context)
        : this(errorType, ex, rawValue)
    {
        Context = context;
    }
}