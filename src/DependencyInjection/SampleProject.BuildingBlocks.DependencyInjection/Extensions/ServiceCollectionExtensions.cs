using System;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using SampleProject.BuildingBlocks.DependencyInjection.Abstractions;

namespace SampleProject.BuildingBlocks.DependencyInjection.Extensions;

/// <summary>
/// The <see cref="IServiceCollection"/> extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds new module to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <param name="module">The instance of <see cref="IModule"/>.</param>
    /// <returns>The same <see cref="IServiceCollection"/> so that multiple calls can be chained.</returns>
    /// <exception cref="ArgumentNullException">Throws an <see cref="ArgumentNullException"/> when the <see cref="IServiceCollection"/> is not set.</exception>
    /// <exception cref="ArgumentNullException">Throws an <see cref="ArgumentNullException"/> when the <see cref="IModule"/> is not set.</exception>
    public static IServiceCollection AddModule([NotNull] this IServiceCollection services, IModule module)
    {
        if (services is null) throw new ArgumentNullException(nameof(services));
        if (module is null) throw new ArgumentNullException(nameof(module));

        module.Load(services);
        return services;
    }
}
