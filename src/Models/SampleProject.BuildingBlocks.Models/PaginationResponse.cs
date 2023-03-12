using System.Collections.Generic;

namespace SampleProject.BuildingBlocks.Models;

/// <summary>
/// A generic Paginated Response Model.
/// </summary>
/// <typeparam name="T"></typeparam>
public class PaginationResponse<T>
{
    /// <summary>
    /// The Data.
    /// </summary>
    public IEnumerable<T> Data { get; set; }

    /// <summary>
    /// The Total Count.
    /// </summary>
    public long TotalCount { get; set; }
}
