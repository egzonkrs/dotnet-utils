using Microsoft.Extensions.Options;
using SampleProject.BuildingBlocks.Options.Abstractions;

namespace SampleProject.BuildingBlocks.Options.Internal;

/// <inheritdoc />
public class OptionsProvider<T> : IOptionsProvider<T> where T : class, new()
{
    private readonly IOptionsSnapshot<T> _optionsAccessor;

    /// <summary>
    /// The Constructor.
    /// </summary>
    /// <param name="optionsAccessor">The <see cref="IOptionsSnapshot{TOptions}"/>.</param>
    public OptionsProvider(IOptionsSnapshot<T> optionsAccessor)
    {
        _optionsAccessor = optionsAccessor;
    }
    
    /// <inheritdoc />
    public T GetOptions(string name)
    {
        throw new System.NotImplementedException();
    }
}