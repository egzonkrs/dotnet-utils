using System;
using SampleProject.Tools.Data.Abstractions;

namespace SampleProject.Tools.Tests.Unit.Mocks;

public class MockEntity : IEntity
{
    public Guid Id { get; set; }
    public string TenantId { get; set; }
}