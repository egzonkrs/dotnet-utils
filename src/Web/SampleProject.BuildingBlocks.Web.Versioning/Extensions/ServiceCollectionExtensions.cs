using System;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace SampleProject.BuildingBlocks.Web.Versioning.Extensions;

/// <summary>
/// The <see cref="IServiceCollection" /> Extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds Api Explorer Versioning.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to. </param>
    /// <param name="configureOptions">A delegate that is used to configure the <see cref="ApiVersioningOptions" />.</param>
    /// <returns>The same <see cref="IServiceCollection"/> so that multiple calls can be chained.</returns>
    /// <exception cref="ArgumentNullException">Throws an <see cref="ArgumentNullException"/> when the <see cref="IServiceCollection"/> is not set.</exception>
    public static IServiceCollection AddApiExplorerVersioning(
        [NotNull] this IServiceCollection services,
        Action<ApiVersioningOptions> configureOptions = null)
    {
        if (services is null) throw new ArgumentNullException(nameof(services));

        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            configureOptions?.Invoke(options);
        });

        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
}