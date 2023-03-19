using SampleProject.Tools.Data.Abstractions;
using SampleProject.Tools.Tests.Unit.Mocks;

namespace SampleProject.Tools.Tests.Unit.Specifications;

public class TenantSpecification : ExpressionSpecification<MockEntity>
{
    public TenantSpecification(string tenantId)
    {
        Expression = entity => entity.TenantId == tenantId;
    }
}