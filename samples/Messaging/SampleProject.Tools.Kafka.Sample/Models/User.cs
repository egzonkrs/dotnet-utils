using System;

namespace SampleProject.Tools.Kafka.Sample.Models;

public class User
{
    public Guid UserId { get; set; }
    
    public string Name { get; set; }
    
    public int Age { get; set; }
    
    public DateTime JoinedAt { get; set; }
}