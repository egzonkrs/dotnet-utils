using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SampleProject.Utils.Tests.Unit.Mocks;
using Xunit;

namespace SampleProject.Utils.Tests.Unit.Tests;

[Trait("Category", "Integration")]
public class EfDbContextPoolRepositoryTests : AbstractEfRepositoryTests
{
    public EfDbContextPoolRepositoryTests() : base(services =>
        services.AddDbContextPool<MockDbContext>(x => { x.UseInMemoryDatabase(Guid.NewGuid().ToString()); }))
    {
    }

    // public EfDbContextRepositoryTests() : base(services =>
    //     services.AddDbContext<MockDbContext>(x =>
    //     {
    //         x.UseInMemoryDatabase(Guid.NewGuid().ToString());
    //     }))
    // {
    // }
}