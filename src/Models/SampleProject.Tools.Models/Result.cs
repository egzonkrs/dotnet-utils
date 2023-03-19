using System.Collections.Generic;

namespace SampleProject.Tools.Models;

/// <summary>
/// A generic Result Model.
/// </summary>
public class Result
{
    internal readonly List<string> ErrorCodesInternal = new List<string>();

    /// <summary>
    /// A value to indicate whether the Result contains Errors.
    /// </summary>
    public bool IsError { get; internal set; } = false;

    /// <summary>
    /// A list of Error Codes.
    /// </summary>
    public string[] ErrorCodes => ErrorCodesInternal.ToArray();

    /// <summary>
    /// Success operation.
    /// </summary>
    /// <returns>The <see cref="Result"/>.</returns>
    public static Result Success() => new Result();

    /// <summary>
    /// Fail Operation.
    /// </summary>
    /// <param name="errorCodes">The Error Codes.</param>
    /// <returns>The <see cref="FailResult"/>.</returns>
    public static FailResult Fail(params string[] errorCodes)
    {
        var result = new FailResult { IsError = true };

        if (errorCodes.Length > 0)
        {
            result.ErrorCodesInternal.AddRange(errorCodes);
        }

        return result;
    }

    /// <summary>
    /// Not Found Operation.
    /// </summary>
    /// <returns>The <see cref="Result"/>.</returns>
    public static Result NotFound() => Fail(CommonErrorCodes.NotFound);

    /// <summary>
    /// Conflict operation.
    /// </summary>
    /// <returns>The <see cref="Result"/>.</returns>
    public static Result Conflict() => Fail(CommonErrorCodes.Conflict);

    /// <summary>
    /// Forbidden operation.
    /// </summary>
    /// <returns>The <see cref="Result"/>.</returns>
    public static Result Forbidden() => Fail(CommonErrorCodes.Forbidden);

    /// <summary>
    /// Checks if Result contains an Error Code.
    /// </summary>
    /// <param name="errorCode"></param>
    public bool HasErrorCode(string errorCode) => IsError && ErrorCodesInternal.Contains(errorCode);

    /// <summary>
    /// Checks if one of the Error Codes is NOT_FOUND.
    /// </summary>
    public bool IsNotFound() => HasErrorCode(CommonErrorCodes.NotFound);

    /// <summary>
    /// Checks if one of the Error Codes is CONFLICT.
    /// </summary>
    public bool IsConflict() => HasErrorCode(CommonErrorCodes.Conflict);

    /// <summary>
    /// Checks if one of the Error Codes is FORBIDDEN.
    /// </summary>
    public bool IsForbidden() => HasErrorCode(CommonErrorCodes.Forbidden);
}
