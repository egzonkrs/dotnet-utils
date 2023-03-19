using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SampleProject.Tools.Data.Linq.Extensions;

/// <summary>
/// The <see cref="IServiceCollection"/> extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds Query Filters.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <returns>The same <see cref="IServiceCollection"/> so that multiple calls can be chained.</returns>
    /// <exception cref="ArgumentNullException">Throws an <see cref="ArgumentNullException"/> when the <see cref="IServiceCollection"/> is not set.</exception>
    public static IServiceCollection AddQueryFilters([NotNull] this IServiceCollection services)
    {
        if (services is null) throw new ArgumentNullException(nameof(services));

        services.TryAddSingleton<QueryFiltersAccessor>();

        return services;
    }
}