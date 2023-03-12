namespace SampleProject.BuildingBlocks.Data.Abstractions;

/// <summary>
/// The Entity.
/// </summary>
/// <typeparam name="TPrimaryKey">The Primary Key Data Type.</typeparam>
public interface IEntity<TPrimaryKey>
{
    /// <summary>
    /// The Primary Key.
    /// </summary>
    TPrimaryKey Id { get; set; }
}
