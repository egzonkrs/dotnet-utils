using System;

namespace SampleProject.Tools.Kafka.Sample.Models;

public class CreateUser
{
    public Guid UserId { get; set; }
    
    public string Name { get; set; }
    
    public int Age { get; set; }
    
    public DateTime JoinedAt { get; set; }

    public override string ToString()
    {
        return $"{nameof(UserId)}: {UserId}, {nameof(Name)}: {Name}, {nameof(Age)}: {Age}, {nameof(JoinedAt)}: {JoinedAt}";
    }
}