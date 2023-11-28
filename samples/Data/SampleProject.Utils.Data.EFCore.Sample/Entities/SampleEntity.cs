using System;
using SampleProject.Utils.Data.Abstractions;

namespace SampleProject.Utils.Data.EFCore.Sample.Entities;

public class SampleEntity : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedOn { get; set; }
}
