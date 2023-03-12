using System;
using Microsoft.EntityFrameworkCore;
using SampleProject.BuildingBlocks.Data.Abstractions;

namespace SampleProject.BuildingBlocks.Data.EntityFrameworkCore.Abstractions;

/// <summary>
/// The EF Core Repository.
/// </summary>
/// <typeparam name="TEntity">The Entity Date Type.</typeparam>
public abstract class EfRepositoryBase<TEntity> : EfRepositoryBase<Guid, TEntity>, IRepository<TEntity>
    where TEntity : class, IEntity
{
    /// <inheritdoc/>
    protected EfRepositoryBase(DbContext dbContext) : base(dbContext)
    {
    }
}
