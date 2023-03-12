namespace SampleProject.BuildingBlocks.Options.Abstractions;

/// <summary>
/// Generic Options Provider.
/// </summary>
public interface IOptionsProvider<T> where T : class
{
    /// <summary>
    /// Gets the Options.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <returns></returns>
    T GetOptions(string name);
}