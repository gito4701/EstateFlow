using EstateFlow.Domain.Exceptions;
using EstateFlow.Domain.Owners;
using Xunit;

namespace EstateFlow.Domain.Tests;

public class OwnerTests
{
    [Fact]
    public void Create_ShouldCreateOwnerWithApprovedIdentity()
    {
        var owner = Owner.Create(OwnerId.NewId(), "Sample Owner");

        Assert.NotNull(owner);
        Assert.Equal("Sample Owner", owner.Name);
    }

    [Fact]
    public void Create_WithEmptyName_ShouldThrow()
    {
        var ex = Assert.Throws<InvalidOwnerException>(() => Owner.Create(OwnerId.NewId(), " "));
        Assert.Contains("required", ex.Message);
    }

    [Fact]
    public void Create_WithEmptyId_ShouldThrow()
    {
        var ex = Assert.Throws<ArgumentException>(() => Owner.Create(default, "Sample Owner"));
        Assert.Contains("OwnerId", ex.Message);
    }
}
