namespace SampleProject.Tools.Http;

/// <summary>
/// The Default Http Client Options.
/// </summary>
public class DefaultClientOptions
{
    /// <summary>
    /// The configuration default section name.
    /// </summary>
    public const string DefaultSectionName = "HttpClient";

    /// <summary>
    /// The Base Url.
    /// </summary>
    public string BaseUrl { get; set; }
}