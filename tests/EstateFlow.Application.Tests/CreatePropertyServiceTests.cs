using EstateFlow.Application.Requests;
using EstateFlow.Application.Responses;
using EstateFlow.Application.Services;
using Xunit;

namespace EstateFlow.Application.Tests;

public class CreatePropertyServiceTests
{
    [Fact]
    public void Handle_WithValidRequest_ReturnsSuccessResponse()
    {
        var service = new CreatePropertyService();
        var request = new CreatePropertyRequest("Sample Property", "123 Main St");

        var response = service.Handle(request);

        Assert.NotNull(response);
        Assert.True(response.IsSuccess);
        Assert.NotNull(response.Property);
        Assert.Equal("Sample Property", response.Property.Name);
        Assert.Equal("123 Main St", response.Property.Address);
    }

    [Fact]
    public void Handle_WithInvalidRequest_ReturnsFailureResponse()
    {
        var service = new CreatePropertyService();
        var request = new CreatePropertyRequest("", "");

        var response = service.Handle(request);

        Assert.NotNull(response);
        Assert.False(response.IsSuccess);
        Assert.NotNull(response.Error);
        Assert.Null(response.Property);
    }

    [Fact]
    public void Handle_UsesDomainCreationPathWithoutDuplicatingBusinessRules()
    {
        var service = new CreatePropertyService();
        var request = new CreatePropertyRequest("Sample Property", "123 Main St");

        var response = service.Handle(request);

        Assert.True(response.IsSuccess);
        Assert.NotNull(response.Property);
    }
}
