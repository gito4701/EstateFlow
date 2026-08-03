using EstateFlow.Domain.Exceptions;
using EstateFlow.Domain.Users;
using Xunit;

namespace EstateFlow.Domain.Tests;

public class UserIdentityTests
{
    [Fact]
    public void Create_ShouldCreateUserIdentityWithApprovedIdentityKind()
    {
        var identity = UserIdentity.Create(UserIdentityId.NewId(), "Sample User", UserIdentityKind.Person);

        Assert.NotNull(identity);
        Assert.Equal("Sample User", identity.Name);
        Assert.Equal(UserIdentityKind.Person, identity.Kind);
    }

    [Fact]
    public void Create_WithEmptyName_ShouldThrow()
    {
        var ex = Assert.Throws<InvalidUserIdentityException>(() => UserIdentity.Create(UserIdentityId.NewId(), " ", UserIdentityKind.Person));
        Assert.Contains("required", ex.Message);
    }

    [Fact]
    public void Create_WithEmptyId_ShouldThrow()
    {
        var ex = Assert.Throws<ArgumentException>(() => UserIdentity.Create(default, "Sample User", UserIdentityKind.Person));
        Assert.Contains("UserIdentityId", ex.Message);
    }
}
