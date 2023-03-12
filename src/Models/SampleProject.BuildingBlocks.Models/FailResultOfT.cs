namespace SampleProject.BuildingBlocks.Models;

/// <summary>
/// A generic Failed Result.
/// </summary>
/// <typeparam name="T"></typeparam>
public class FailResult<T> : Result<T>
{
    /// <summary>
    /// Adds and Error Code to the <see cref="FailResult{T}"/>
    /// </summary>
    /// <param name="errorCode">The Error Code.</param>
    public void AddErrorCode(string errorCode)
    {
        ErrorCodesInternal.Add(errorCode);
    }
}
