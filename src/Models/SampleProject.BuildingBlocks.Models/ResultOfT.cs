namespace SampleProject.BuildingBlocks.Models;

/// <summary>
/// A generic Result Model.
/// </summary>
/// <typeparam name="T"></typeparam>
public class Result<T> : Result
{
    /// <summary>
    /// The Data.
    /// </summary>
    public T Data { get; set; }

    /// <summary>
    /// Success operation.
    /// </summary>
    /// <param name="data">The Data.</param>
    /// <returns>The <see cref="Result{T}"/>.</returns>
    public static Result<T> Success(T data) => new Result<T> { Data = data };

    /// <summary>
    /// Fail Operation.
    /// </summary>
    /// <param name="errorCodes">The Error Codes.</param>
    /// <returns>The <see cref="FailResult{T}"/>.</returns>
    public new static FailResult<T> Fail(params string[] errorCodes)
    {
        var result = new FailResult<T> { IsError = true };

        if (errorCodes.Length > 0)
        {
            result.ErrorCodesInternal.AddRange(errorCodes);
        }

        return result;
    }

    /// <summary>
    /// Fail Operation.
    /// </summary>
    /// <param name="data">The Data.</param>
    /// <param name="errorCodes">The Error Codes.</param>
    /// <returns>The <see cref="FailResult{T}"/>.</returns>
    public static FailResult<T> Fail(T data, params string[] errorCodes)
    {
        var result = Fail(errorCodes);
        result.Data = data;

        return result;
    }

    /// <summary>
    /// Not Found Operation.
    /// </summary>
    /// <returns>The <see cref="FailResult{T}"/>.</returns>
    public new static FailResult<T> NotFound() => Fail(CommonErrorCodes.NotFound);

    /// <summary>
    /// Conflict operation.
    /// </summary>
    /// <returns>The <see cref="FailResult{T}"/>.</returns>
    public new static FailResult<T> Conflict() => Fail(CommonErrorCodes.Conflict);

    /// <summary>
    /// Forbidden operation.
    /// </summary>
    /// <returns>The <see cref="FailResult{T}"/>.</returns>
    public new static FailResult<T> Forbidden() => Fail(CommonErrorCodes.Forbidden);
}
