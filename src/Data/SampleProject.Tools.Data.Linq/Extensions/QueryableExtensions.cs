using System.Linq;
using SampleProject.Tools.Data.Abstractions;

namespace SampleProject.Tools.Data.Linq.Extensions;

/// <summary>
/// The <see cref="IQueryable{TEntity}"/> extensions.
/// </summary>
public static class QueryableExtensions
{
    /// <summary>
    /// Filters data using the Specification Pattern.
    /// </summary>
    /// <param name="queryable">The <see cref="IQueryable{TEntity}"/></param>
    /// <param name="specification">The <see cref="ISpecification{TEntity}"/></param>
    /// <typeparam name="TEntity">The Entity.</typeparam>
    /// <returns>The <see cref="IQueryable{TEntity}"/>.</returns>
    public static IQueryable<TEntity> Where<TEntity>(this IQueryable<TEntity> queryable,
        ISpecification<TEntity> specification)
    {
        if (specification is ExpressionSpecification<TEntity> expressionSpecification)
        {
            return queryable.Where(expressionSpecification.Expression);
        }

        return queryable
            .AsEnumerable()
            .Where(specification.IsSatisfiedBy)
            .AsQueryable();
    }
}