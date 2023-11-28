using System;
using SampleProject.Utils.Data.Abstractions;

namespace SampleProject.Utils.Tests.Unit.Mocks;

public class MockEntity : IEntity
{
    public Guid Id { get; set; }
    public string TenantId { get; set; }
}