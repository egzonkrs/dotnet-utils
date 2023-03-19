using System;

namespace SampleProject.Tools.Data.Abstractions;

/// <summary>
/// The Repository with Primary Key of Data Type long.
/// </summary>
/// <typeparam name="TEntity">The Entity Data Type.</typeparam>
public interface IRepository<TEntity> : IRepository<Guid, TEntity> where TEntity : class, IEntity
{
}