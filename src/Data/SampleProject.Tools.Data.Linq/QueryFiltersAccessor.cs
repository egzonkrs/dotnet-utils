using System.Collections.Generic;
using SampleProject.Tools.Data.Linq.Abstractions;

namespace SampleProject.Tools.Data.Linq;

/// <summary>
/// Query Filters Accessor.
/// </summary>
public class QueryFiltersAccessor
{
    /// <summary>
    /// The Query Filters.
    /// </summary>
    public readonly IEnumerable<IQueryFilter> QueryFilters;

    /// <summary>
    /// Constructor.
    /// </summary>
    public QueryFiltersAccessor(IEnumerable<IQueryFilter> queryFilters)
    {
        QueryFilters = queryFilters;
    }
}