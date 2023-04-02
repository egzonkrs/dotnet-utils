using SampleProject.Tools.Data.Abstractions;
using SampleProject.Tools.Data.EFCore.Sample.Entities;

namespace SampleProject.Tools.Data.EFCore.Sample.Repositories;

public interface ISampleRepository : IRepository<SampleEntity>
{
}


