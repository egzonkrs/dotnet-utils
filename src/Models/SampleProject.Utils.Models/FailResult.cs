namespace SampleProject.Utils.Models;

/// <summary>
/// The Fail Result.
/// </summary>
public class FailResult : Result
{
    /// <summary>
    /// Adds and Error Code to the <see cref="FailResult"/>
    /// </summary>
    /// <param name="errorCode">The Error Code.</param>
    public void AddErrorCode(string errorCode)
    {
        ErrorCodesInternal.Add(errorCode);
    }
}
