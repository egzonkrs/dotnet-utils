using Microsoft.Extensions.DependencyInjection;

namespace SampleProject.Utils.DependencyInjection.Abstractions;

/// <summary>
/// The Dependency Injection Module.
/// </summary>
public interface IModule
{
    /// <summary>
    /// Loads the Dependency Injection Registrations.
    /// </summary>
    /// <param name="services"></param>
    void Load(IServiceCollection services);
}