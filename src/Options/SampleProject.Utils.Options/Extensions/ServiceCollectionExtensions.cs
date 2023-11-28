using System;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SampleProject.Utils.Options.Abstractions;
using SampleProject.Utils.Options.Internal;
using OptionsConstants = Microsoft.Extensions.Options.Options;

namespace SampleProject.Utils.Options.Extensions;

/// <summary>
/// The <see cref="IServiceCollection" /> Extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the Default Options Provider.
    /// </summary>
    /// <typeparam name="T">The Options Type.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <param name="configurationSection">The <see cref="IConfigurationSection"/> which binds to the Options.</param>
    /// <param name="name">The Name.</param>
    /// <returns>The same <see cref="IServiceCollection"/> so that multiple calls can be chained.</returns>
    public static IServiceCollection AddDefaultOptionsProvider<T>([NotNull] this IServiceCollection services,
        [NotNull] IConfigurationSection configurationSection, string name = null) where T : class, new()
    {
        if (services is null) throw new ArgumentNullException(nameof(services));
        if (configurationSection is null) throw new ArgumentNullException(nameof(configurationSection));

        name ??= OptionsConstants.DefaultName;
        
        services.TryAddScoped<IOptionsProvider<T>, OptionsProvider<T>>();
        services.Configure<T>(name, configurationSection);

        return services;
    }
}