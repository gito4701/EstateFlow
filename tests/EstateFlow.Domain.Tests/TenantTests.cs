using EstateFlow.Domain.Exceptions;
using EstateFlow.Domain.Tenants;
using Xunit;

namespace EstateFlow.Domain.Tests;

public class TenantTests
{
    [Fact]
    public void Create_ShouldCreateTenantWithApprovedIdentity()
    {
        var tenant = Tenant.Create(TenantId.NewId(), "Sample Tenant");

        Assert.NotNull(tenant);
        Assert.Equal("Sample Tenant", tenant.Name);
    }

    [Fact]
    public void Create_WithEmptyName_ShouldThrow()
    {
        var ex = Assert.Throws<InvalidTenantException>(() => Tenant.Create(TenantId.NewId(), " "));
        Assert.Contains("required", ex.Message);
    }

    [Fact]
    public void Create_WithEmptyId_ShouldThrow()
    {
        var ex = Assert.Throws<ArgumentException>(() => Tenant.Create(default, "Sample Tenant"));
        Assert.Contains("TenantId", ex.Message);
    }
}
