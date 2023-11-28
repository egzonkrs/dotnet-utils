using SampleProject.Utils.Data.EntityFrameworkCore.Abstractions;
using SampleProject.Utils.Data.EFCore.Sample.Data;
using SampleProject.Utils.Data.EFCore.Sample.Entities;

namespace SampleProject.Utils.Data.EFCore.Sample.Repositories;

public class SampleRepository : EfRepositoryBase<SampleEntity>, ISampleRepository
{
    public SampleRepository(SampleDbContext sampleDbContext) : base(sampleDbContext)
    {
    }
}
