using SampleProject.Utils.Data.Abstractions;
using SampleProject.Utils.Tests.Unit.Mocks;

namespace SampleProject.Utils.Tests.Unit.Specifications;

public class TenantSpecification : ExpressionSpecification<MockEntity>
{
    public TenantSpecification(string tenantId)
    {
        Expression = entity => entity.TenantId == tenantId;
    }
}