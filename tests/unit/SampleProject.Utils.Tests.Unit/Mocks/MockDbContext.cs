using Microsoft.EntityFrameworkCore;

namespace SampleProject.Utils.Tests.Unit.Mocks;

public class MockDbContext : DbContext
{
    public MockDbContext(DbContextOptions<MockDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    public DbSet<MockEntity> MockEntities { get; set; }
}