using SampleProject.BuildingBlocks.Data.EFCore.Sample.Data;
using SampleProject.BuildingBlocks.Data.EFCore.Sample.Entities;
using SampleProject.BuildingBlocks.Data.EntityFrameworkCore.Abstractions;

namespace SampleProject.BuildingBlocks.Data.EFCore.Sample.Repositories;

public class SampleRepository : EfRepositoryBase<SampleEntity>, ISampleRepository
{
    public SampleRepository(SampleDbContext sampleDbContext) : base(sampleDbContext)
    {
    }
}
