using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SampleProject.Utils.Data.EFCore.Sample.Data;
using SampleProject.Utils.Data.EFCore.Sample.Entities;
using SampleProject.Utils.Data.EFCore.Sample.Repositories;
using SampleProject.Utils.DependencyInjection.Abstractions;
using SampleProject.Utils.Data.EntityFrameworkCore.Extensions;

namespace SampleProject.Utils.Data.EFCore.Sample.Modules;

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