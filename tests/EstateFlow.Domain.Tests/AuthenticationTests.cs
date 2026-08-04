using EstateFlow.Domain.Authentications;
using EstateFlow.Domain.Exceptions;
using Xunit;

namespace EstateFlow.Domain.Tests;

public class AuthenticationTests
{
    [Fact]
    public void Create_ShouldCreateAuthenticationWithApprovedKind()
    {
        var authentication = Authentication.Create(AuthenticationId.NewId(), "Sample Authentication", AuthenticationKind.Identity);

        Assert.NotNull(authentication);
        Assert.Equal("Sample Authentication", authentication.Name);
        Assert.Equal(AuthenticationKind.Identity, authentication.Kind);
    }

    [Fact]
    public void Create_WithEmptyName_ShouldThrow()
    {
        var ex = Assert.Throws<InvalidAuthenticationException>(() => Authentication.Create(AuthenticationId.NewId(), " ", AuthenticationKind.Identity));
        Assert.Contains("required", ex.Message);
    }

    [Fact]
    public void Create_WithEmptyId_ShouldThrow()
    {
        var ex = Assert.Throws<ArgumentException>(() => Authentication.Create(default, "Sample Authentication", AuthenticationKind.Identity));
        Assert.Contains("AuthenticationId", ex.Message);
    }
}
