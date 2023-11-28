namespace SampleProject.Utils.Caching.Abstractions.Requests;

/// <summary>
/// The Base Request.
/// </summary>
public abstract class BaseRequest
{
    /// <summary>
    /// The Key of the Request.
    /// </summary>
    public string Key { get; set; }
    
    // public ItemPolicy ItemPolicy { get; set; }
}