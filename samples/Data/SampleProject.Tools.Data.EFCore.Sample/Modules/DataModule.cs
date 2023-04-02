using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SampleProject.Tools.Data.EFCore.Sample.Data;
using SampleProject.Tools.Data.EFCore.Sample.Entities;
using SampleProject.Tools.Data.EFCore.Sample.Repositories;
using SampleProject.Tools.DependencyInjection.Abstractions;
using SampleProject.Tools.Data.EntityFrameworkCore.Extensions;

namespace SampleProject.Tools.Data.EFCore.Sample.Modules;

public class DataModule : IModule
{
    public void Load(IServiceCollection services)
    {
        services.AddDbContextPool<SampleDbContext>(opt =>
        {
            opt.UseInMemoryDatabase(Guid.Empty.ToString());
        });

        services.AddEfCoreUnitOfWork<SampleDbContext>();
        services.AddEfCoreRepository<SampleEntity, ISampleRepository, SampleRepository>();
    }
}