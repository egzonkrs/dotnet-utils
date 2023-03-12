using System;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SampleProject.BuildingBlocks.Data.EFCore.Sample.Data;
using SampleProject.BuildingBlocks.Data.EFCore.Sample.Entities;
using SampleProject.BuildingBlocks.Data.EFCore.Sample.Repositories;
using SampleProject.BuildingBlocks.Data.EntityFrameworkCore.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContextPool<SampleDbContext>(opt =>
{
    opt.UseInMemoryDatabase(Guid.Empty.ToString());
});

builder.Services.AddEfCoreUnitOfWork<SampleDbContext>();
builder.Services.AddEfCoreRepository<SampleEntity, ISampleRepository, SampleRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
