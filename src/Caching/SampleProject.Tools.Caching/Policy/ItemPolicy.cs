using System.Reflection;

namespace SampleProject.Tools.Caching.Policy;

/// <summary>
/// Represents cache item settings for each providers.
/// </summary>
public class ItemPolicy
{
    public ItemPolicy Default = new ItemPolicy();
    
    /// <summary>
    /// Name of the Policy.
    /// </summary>
    public string Name { get; set; }
}