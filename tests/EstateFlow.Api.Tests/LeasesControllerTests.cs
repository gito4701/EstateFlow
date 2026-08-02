using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EstateFlow.Api.Controllers;
using EstateFlow.Application.Persistence;
using EstateFlow.Application.Requests;
using EstateFlow.Application.Services;
using EstateFlow.Domain.Leases;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace EstateFlow.Api.Tests;

public class LeasesControllerTests
{
    [Fact]
    public void Create_WithValidRequest_ReturnsCreatedResult()
    {
        var repository = new FakeLeaseRepository();
        var createService = new CreateLeaseService(repository);
        var getService = new GetLeaseService(repository);
        var controller = new LeasesController(createService, getService);

        var result = controller.Create(new CreateLeaseRequest("Sample Lease"));

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(201, createdResult.StatusCode);
    }

    [Fact]
    public void GetAll_ReturnsCollection()
    {
        var repository = new FakeLeaseRepository();
        var createService = new CreateLeaseService(repository);
        var getService = new GetLeaseService(repository);
        var controller = new LeasesController(createService, getService);

        controller.Create(new CreateLeaseRequest("Lease One"));
        controller.Create(new CreateLeaseRequest("Lease Two"));

        var result = controller.GetAll();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var payload = Assert.IsAssignableFrom<IEnumerable<object>>(okResult.Value!);

        Assert.Equal(2, payload.Count());
    }

    private sealed class FakeLeaseRepository : ILeaseRepository
    {
        private readonly List<Lease> _leases = new();

        public Task AddAsync(Lease aggregate, CancellationToken cancellationToken = default)
        {
            _leases.Add(aggregate);
            return Task.CompletedTask;
        }

        public Task<IReadOnlyList<Lease>> ListAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult<IReadOnlyList<Lease>>(_leases.ToList());
        }

        public Task<Lease?> GetByIdAsync(LeaseId id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_leases.FirstOrDefault(lease => lease.Id.Equals(id)));
        }
    }
}
