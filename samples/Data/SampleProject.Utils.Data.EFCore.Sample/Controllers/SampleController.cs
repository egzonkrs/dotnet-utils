using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SampleProject.Utils.Data.Abstractions;
using SampleProject.Utils.Data.EFCore.Sample.Data;
using SampleProject.Utils.Data.EFCore.Sample.Entities;
using SampleProject.Utils.Data.EFCore.Sample.Repositories;
using SampleProject.Utils.Models;

namespace SampleProject.Utils.Data.EFCore.Sample.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SampleController : ControllerBase
{
    private readonly ISampleRepository _sampleRepository;
    private readonly IUnitOfWork<SampleDbContext> _unitOfWork;

    public SampleController(ISampleRepository sampleRepository, IUnitOfWork<SampleDbContext> unitOfWork)
    {
        _sampleRepository = sampleRepository;
        _unitOfWork = unitOfWork;
    }

    [HttpGet("insert-one")]
    public async Task<Result<SampleEntity>> InsertOneSample()
    {
        var entity = new SampleEntity
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            CreatedOn = DateTime.UtcNow
        };

        await _sampleRepository.InsertAsync(entity);
        var rowsAffected = await _unitOfWork.SaveChangesAsync();

        return rowsAffected > 0 
            ? Result<SampleEntity>.Success(entity) 
            : Result<SampleEntity>.Fail("Failed to save the entity.");
    }

    [HttpGet("get-all")]
    public async Task<Result<IEnumerable<SampleEntity>>> GetAllSamples()
    {
        var samples = await _sampleRepository.GetAllAsync();

        if (samples is null || !samples.Any())
        {
            return Result<IEnumerable<SampleEntity>>.Fail("No samples found.");
        }

        return Result<IEnumerable<SampleEntity>>.Success(samples);
    }
}