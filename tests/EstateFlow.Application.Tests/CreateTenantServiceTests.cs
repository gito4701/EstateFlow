using System.Threading;
using System.Threading.Tasks;
using EstateFlow.Application.Persistence;
using EstateFlow.Application.Requests;
using EstateFlow.Application.Responses;
using EstateFlow.Application.Services;
using EstateFlow.Domain.Tenants;
using Xunit;

namespace EstateFlow.Application.Tests;

public class CreateTenantServiceTests
{
    [Fact]
    public void Handle_WithValidRequest_PersistsTenantAndReturnsSuccessResponse()
    {
        var repository = new FakeTenantRepository();
        var service = new CreateTenantService(repository);
        var request = new CreateTenantRequest("Sample Tenant");

        var response = service.Handle(request);

        Assert.NotNull(response);
        Assert.True(response.IsSuccess);
        Assert.NotNull(response.Tenant);
        Assert.True(response.TenantId.HasValue);
        Assert.Equal(response.Tenant.Id, response.TenantId.Value);
        Assert.Same(response.Tenant, repository.AddedTenant);
        Assert.Equal("Sample Tenant", response.Tenant.Name);
    }

    [Fact]
    public void Handle_WithInvalidRequest_ReturnsFailureResponseWithoutPersisting()
    {
        var repository = new FakeTenantRepository();
        var service = new CreateTenantService(repository);
        var request = new CreateTenantRequest(string.Empty);

        var response = service.Handle(request);

        Assert.NotNull(response);
        Assert.False(response.IsSuccess);
        Assert.NotNull(response.Error);
        Assert.Null(response.Tenant);
        Assert.False(response.TenantId.HasValue);
        Assert.Null(repository.AddedTenant);
    }

    private sealed class FakeTenantRepository : ITenantRepository
    {
        public Tenant? AddedTenant { get; private set; }

        public Task AddAsync(Tenant aggregate, CancellationToken cancellationToken = default)
        {
            AddedTenant = aggregate;
            return Task.CompletedTask;
        }
    }
}
