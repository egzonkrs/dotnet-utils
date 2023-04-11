using System.Reflection;
using SampleProject.Tools.Caching.Abstractions;

namespace SampleProject.Tools.Caching.Policy;

/// <summary>
/// Represents cache item settings for each providers.
/// </summary>
public class ItemPolicy
{
    /// <summary>
    /// The default ItemPolicy. Means the item is never cached.
    /// </summary>
    public ItemPolicy Default = new ItemPolicy {  Name = "(default)"};
    
    /// <summary>
    /// Name of the Policy.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The local cache policy.
    /// </summary>
    public ICachePolicy Local { get; set; }
    
    /// <summary>
    /// A Fluent interface to create a new item policy.
    /// </summary>
    public static IItemPolicyBuilder Builder => new ItemPolicyBuilder();
}