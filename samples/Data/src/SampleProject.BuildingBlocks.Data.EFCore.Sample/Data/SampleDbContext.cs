using Microsoft.EntityFrameworkCore;
using SampleProject.BuildingBlocks.Data.EFCore.Sample.Entities;

namespace SampleProject.BuildingBlocks.Data.EFCore.Sample.Data;

public class SampleDbContext : DbContext
{
    public SampleDbContext(DbContextOptions<SampleDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    public DbSet<SampleEntity> Samples { get; set; }
}
