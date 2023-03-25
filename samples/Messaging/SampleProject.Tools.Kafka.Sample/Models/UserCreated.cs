namespace SampleProject.Tools.Kafka.Sample.Models;

public class UserCreated
{
    public Guid UserId { get; set; }

    public string Name { get; set; }

    public int Age { get; set; }

    public DateTime JoinedAt { get; set; }
}