using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EstateFlow.Api.Controllers;
using EstateFlow.Application.Persistence;
using EstateFlow.Application.Requests;
using EstateFlow.Application.Services;
using EstateFlow.Domain.Tenants;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace EstateFlow.Api.Tests;

public class TenantsControllerTests
{
    [Fact]
    public void Create_WithValidRequest_ReturnsCreatedResult()
    {
        var repository = new FakeTenantRepository();
        var createService = new CreateTenantService(repository);
        var getService = new GetTenantService(repository);
        var controller = new TenantsController(createService, getService);

        var result = controller.Create(new CreateTenantRequest("Sample Tenant"));

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(201, createdResult.StatusCode);
    }

    [Fact]
    public void GetAll_ReturnsCollection()
    {
        var repository = new FakeTenantRepository();
        var createService = new CreateTenantService(repository);
        var getService = new GetTenantService(repository);
        var controller = new TenantsController(createService, getService);

        controller.Create(new CreateTenantRequest("Tenant One"));
        controller.Create(new CreateTenantRequest("Tenant Two"));

        var result = controller.GetAll();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var payload = Assert.IsAssignableFrom<IEnumerable<object>>(okResult.Value!);

        Assert.Equal(2, payload.Count());
    }

    private sealed class FakeTenantRepository : ITenantRepository
    {
        private readonly List<Tenant> _tenants = new();

        public Task AddAsync(Tenant aggregate, CancellationToken cancellationToken = default)
        {
            _tenants.Add(aggregate);
            return Task.CompletedTask;
        }

        public Task<IReadOnlyList<Tenant>> ListAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult<IReadOnlyList<Tenant>>(_tenants.ToList());
        }

        public Task<Tenant?> GetByIdAsync(TenantId id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_tenants.FirstOrDefault(tenant => tenant.Id.Equals(id)));
        }
    }
}
