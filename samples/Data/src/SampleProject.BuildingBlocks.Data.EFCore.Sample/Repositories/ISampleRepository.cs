using SampleProject.BuildingBlocks.Data.EFCore.Sample.Entities;
using SampleProject.BuildingBlocks.Data.Abstractions;

namespace SampleProject.BuildingBlocks.Data.EFCore.Sample.Repositories;

public interface ISampleRepository : IRepository<SampleEntity>
{
}


