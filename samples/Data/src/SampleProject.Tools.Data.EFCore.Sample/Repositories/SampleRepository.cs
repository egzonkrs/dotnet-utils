using SampleProject.Tools.Data.EntityFrameworkCore.Abstractions;
using SampleProject.Tools.Data.EFCore.Sample.Data;
using SampleProject.Tools.Data.EFCore.Sample.Entities;

namespace SampleProject.Tools.Data.EFCore.Sample.Repositories;

public class SampleRepository : EfRepositoryBase<SampleEntity>, ISampleRepository
{
    public SampleRepository(SampleDbContext sampleDbContext) : base(sampleDbContext)
    {
    }
}
