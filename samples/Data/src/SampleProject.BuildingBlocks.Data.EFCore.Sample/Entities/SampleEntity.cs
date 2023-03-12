using System;
using SampleProject.BuildingBlocks.Data.Abstractions;

namespace SampleProject.BuildingBlocks.Data.EFCore.Sample.Entities;

public class SampleEntity : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedOn { get; set; }
}
