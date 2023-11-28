using SampleProject.Utils.Data.Abstractions;
using SampleProject.Utils.Data.EFCore.Sample.Entities;

namespace SampleProject.Utils.Data.EFCore.Sample.Repositories;

public interface ISampleRepository : IRepository<SampleEntity>
{
}


