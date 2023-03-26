using Refit;
using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Tools.Http;
using SampleProject.Tools.Data.Abstractions;
using SampleProject.Tools.Data.EFCore.Sample.Data;
using SampleProject.Tools.Data.EFCore.Sample.Entities;
using SampleProject.Tools.Data.EFCore.Sample.Repositories;

namespace SampleProject.Tools.Data.EFCore.Sample.Controllers;

public interface IMockRefitRestClient
{
    [Get("/users")]
    Task<User> GetUsersAsync();
}

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
        
        var client = HttpClientFactory.Create(x =>
        {
            x.Options.WithBaseAddress("https://random-data-api.com/api/v2");
        });
        
        var service = RestService.For<IMockRefitRestClient>(client);

        // Act
        var actual = await service.GetUsersAsync();

        return Ok(new { Data = actual });
        // return Ok(new { Data = entity, IsSaved = isSaved });
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllSamples()
    {
        return Ok(await _sampleRepository.GetAllAsync());
    }
}

public partial class User
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("uid")]
    public Guid Uid { get; set; }

    [JsonProperty("password")]
    public string Password { get; set; }

    [JsonProperty("first_name")]
    public string FirstName { get; set; }

    [JsonProperty("last_name")]
    public string LastName { get; set; }

    [JsonProperty("username")]
    public string Username { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("avatar")]
    public Uri Avatar { get; set; }

    [JsonProperty("gender")]
    public string Gender { get; set; }

    [JsonProperty("phone_number")]
    public string PhoneNumber { get; set; }

    [JsonProperty("social_insurance_number")]
    public long SocialInsuranceNumber { get; set; }

    [JsonProperty("date_of_birth")]
    public DateTimeOffset DateOfBirth { get; set; }

    [JsonProperty("employment")]
    public Employment Employment { get; set; }

    [JsonProperty("address")]
    public Address Address { get; set; }

    [JsonProperty("credit_card")]
    public CreditCard CreditCard { get; set; }

    [JsonProperty("subscription")]
    public Subscription Subscription { get; set; }
}

public class Address
{
    [JsonProperty("city")]
    public string City { get; set; }

    [JsonProperty("street_name")]
    public string StreetName { get; set; }

    [JsonProperty("street_address")]
    public string StreetAddress { get; set; }

    [JsonProperty("zip_code")]
    public string ZipCode { get; set; }

    [JsonProperty("state")]
    public string State { get; set; }

    [JsonProperty("country")]
    public string Country { get; set; }

    [JsonProperty("coordinates")]
    public Coordinates Coordinates { get; set; }
}

public class Coordinates
{
    [JsonProperty("lat")]
    public double Lat { get; set; }

    [JsonProperty("lng")]
    public double Lng { get; set; }
}

public class CreditCard
{
    [JsonProperty("cc_number")]
    public string CcNumber { get; set; }
}

public class Employment
{
    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("key_skill")]
    public string KeySkill { get; set; }
}

public class Subscription
{
    [JsonProperty("plan")]
    public string Plan { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("payment_method")]
    public string PaymentMethod { get; set; }

    [JsonProperty("term")]
    public string Term { get; set; }
}