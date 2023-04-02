using Refit;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Tools.Models;
using Microsoft.Extensions.Logging;
using SampleProject.Tools.Http.Sample.Models;

namespace SampleProject.Tools.Http.Sample.Controllers;

[ApiController]
[Route("[controller]")]
public class SampleController : ControllerBase
{
    private readonly ILogger<SampleController> _logger;

    public SampleController(ILogger<SampleController> logger)
    {
        _logger = logger;
    }

    [HttpGet("get-random-user", Name = "GetRandomUser")]
    public async Task<Result<RandomUser>> GetRandomUser()
    {
        try
        {
            var client = HttpClientFactory.Create(x =>
            {
                x.Options.WithBaseAddress("https://random-data-api.com/api/v2");
            });
        
            var service = RestService.For<IRandomUserClient>(client);

            var randomUser = await service.GetRandomUserAsync();

            return Result<RandomUser>.Success(randomUser);
        }
        catch (Exception ex)
        {
            return Result<RandomUser>.Fail($"Failed to send the request because: {ex.Message}");
        }
    }
}