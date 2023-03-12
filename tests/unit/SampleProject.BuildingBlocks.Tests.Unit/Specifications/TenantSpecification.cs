using SampleProject.BuildingBlocks.Data.Abstractions;
using SampleProject.BuildingBlocks.Tests.Unit.Mocks;

namespace SampleProject.BuildingBlocks.Tests.Unit.Specifications;

public class TenantSpecification : ExpressionSpecification<MockEntity>
{
    public TenantSpecification(string tenantId)
    {
        Expression = entity => entity.TenantId == tenantId;
    }
}
