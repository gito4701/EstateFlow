using EstateFlow.Domain.Exceptions;
using EstateFlow.Domain.Leases;
using Xunit;

namespace EstateFlow.Domain.Tests;

public class LeaseTests
{
    [Fact]
    public void Create_ShouldCreateLeaseWithApprovedIdentity()
    {
        var lease = Lease.Create(LeaseId.NewId(), "Sample Lease");

        Assert.NotNull(lease);
        Assert.Equal("Sample Lease", lease.Name);
    }

    [Fact]
    public void Create_WithEmptyName_ShouldThrow()
    {
        var ex = Assert.Throws<InvalidLeaseException>(() => Lease.Create(LeaseId.NewId(), " "));
        Assert.Contains("required", ex.Message);
    }

    [Fact]
    public void Create_WithEmptyId_ShouldThrow()
    {
        var ex = Assert.Throws<ArgumentException>(() => Lease.Create(default, "Sample Lease"));
        Assert.Contains("LeaseId", ex.Message);
    }
}
