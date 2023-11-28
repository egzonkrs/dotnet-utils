using SampleProject.Utils.Data.EntityFrameworkCore.Abstractions;

namespace SampleProject.Utils.Tests.Unit.Mocks;

public class MockRepository : EfRepositoryBase<MockEntity>
{
    public MockRepository(MockDbContext dbContext) : base(dbContext)
    {
    }
}