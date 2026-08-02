using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EstateFlow.Api.Controllers;
using EstateFlow.Application.Persistence;
using EstateFlow.Application.Requests;
using EstateFlow.Application.Services;
using EstateFlow.Domain.Owners;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace EstateFlow.Api.Tests;

public class OwnersControllerTests
{
    [Fact]
    public void Create_WithValidRequest_ReturnsCreatedResult()
    {
        var repository = new FakeOwnerRepository();
        var createService = new CreateOwnerService(repository);
        var getService = new GetOwnerService(repository);
        var controller = new OwnersController(createService, getService);

        var result = controller.Create(new CreateOwnerRequest("Sample Owner"));

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(201, createdResult.StatusCode);
    }

    [Fact]
    public void GetAll_ReturnsCollection()
    {
        var repository = new FakeOwnerRepository();
        var createService = new CreateOwnerService(repository);
        var getService = new GetOwnerService(repository);
        var controller = new OwnersController(createService, getService);

        controller.Create(new CreateOwnerRequest("Owner One"));
        controller.Create(new CreateOwnerRequest("Owner Two"));

        var result = controller.GetAll();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var payload = Assert.IsAssignableFrom<IEnumerable<object>>(okResult.Value!);

        Assert.Equal(2, payload.Count());
    }

    private sealed class FakeOwnerRepository : IOwnerRepository
    {
        private readonly List<Owner> _owners = new();

        public Task AddAsync(Owner aggregate, CancellationToken cancellationToken = default)
        {
            _owners.Add(aggregate);
            return Task.CompletedTask;
        }

        public Task<IReadOnlyList<Owner>> ListAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult<IReadOnlyList<Owner>>(_owners.ToList());
        }

        public Task<Owner?> GetByIdAsync(OwnerId id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_owners.FirstOrDefault(owner => owner.Id.Equals(id)));
        }
    }
