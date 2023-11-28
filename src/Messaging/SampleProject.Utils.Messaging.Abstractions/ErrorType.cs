namespace SampleProject.Utils.Messaging.Abstractions;

/// <summary>
/// Generic Error Type.
/// </summary>
public enum ErrorType
{
    /// <summary>
    /// Error during Deserialization.
    /// </summary>
    Deserialization,

    /// <summary>
    /// Error during Processing.
    /// </summary>
    Processing
}