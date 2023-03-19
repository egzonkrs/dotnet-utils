using SampleProject.Tools.Data.EntityFrameworkCore.Abstractions;

namespace SampleProject.Tools.Tests.Unit.Mocks;

public class MockRepository : EfRepositoryBase<MockEntity>
{
    public MockRepository(MockDbContext dbContext) : base(dbContext)
    {
    }
}