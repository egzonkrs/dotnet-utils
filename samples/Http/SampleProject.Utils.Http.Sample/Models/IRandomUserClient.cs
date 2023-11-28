using Refit;
using System.Threading.Tasks;

namespace SampleProject.Utils.Http.Sample.Models;

public interface IRandomUserClient
{
    [Get("/users")]
    Task<RandomUser> GetRandomUserAsync();
}