using SampleProject.BuildingBlocks.Data.EntityFrameworkCore.Abstractions;

namespace SampleProject.BuildingBlocks.Tests.Unit.Mocks;

public class MockRepository : EfRepositoryBase<MockEntity>
{
    public MockRepository(MockDbContext dbContext) : base(dbContext)
    {
    }
}
