using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using SampleProject.Utils.Kafka.Sample.Models;

namespace SampleProject.Utils.Kafka.Sample.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<User> Get()
    {
        var user = new User
        {
            UserId = Guid.NewGuid(),
            Name = "Jim",
            Age = new Random().Next(1, 99),
            JoinedAt = DateTime.UtcNow,
        };

        return new[] { user };
    }
}