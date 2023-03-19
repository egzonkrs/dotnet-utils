using System;
using SampleProject.Tools.Data.Abstractions;

namespace SampleProject.Tools.Data.EFCore.Sample.Entities;

public class SampleEntity : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedOn { get; set; }
}
