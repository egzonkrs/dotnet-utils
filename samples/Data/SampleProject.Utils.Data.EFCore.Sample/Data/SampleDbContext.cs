using Microsoft.EntityFrameworkCore;
using SampleProject.Utils.Data.EFCore.Sample.Entities;

namespace SampleProject.Utils.Data.EFCore.Sample.Data;

public class SampleDbContext : DbContext
{
    public SampleDbContext(DbContextOptions<SampleDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    public DbSet<SampleEntity> Samples { get; set; }
}
