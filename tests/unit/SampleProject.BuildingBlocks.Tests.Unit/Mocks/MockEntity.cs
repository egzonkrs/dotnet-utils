using System;
using SampleProject.BuildingBlocks.Data.Abstractions;

namespace SampleProject.BuildingBlocks.Tests.Unit.Mocks;

public class MockEntity : IEntity
{
    public Guid Id { get; set; }
    public string TenantId { get; set; }
}
