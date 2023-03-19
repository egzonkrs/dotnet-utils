using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Tools.Data.Abstractions;
using SampleProject.Tools.Data.EFCore.Sample.Data;
using SampleProject.Tools.Data.EFCore.Sample.Entities;
using SampleProject.Tools.Data.EFCore.Sample.Repositories;

namespace SampleProject.Tools.Data.EFCore.Sample.Controllers;

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
    public async Task<IActionResult> InsertOneSample()
    {
        var entity = new SampleEntity { Id = Guid.NewGuid(), Name = "Test", CreatedOn = DateTime.UtcNow };

        var entityId = await _sampleRepository.InsertAsync(entity);
        var isSaved = await _unitOfWork.SaveChangesAsync();

        return Ok(new { Data = entity, IsSaved = isSaved });
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllSamples()
    {
        return Ok(await _sampleRepository.GetAllAsync());
    }
}
