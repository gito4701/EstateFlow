using EstateFlow.Api.Controllers;
using EstateFlow.Application.Requests;
using EstateFlow.Application.Services;
using EstateFlow.Domain.Properties;
using EstateFlow.Application.Persistence;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace EstateFlow.Api.Tests;

public class PropertiesControllerTests
{
    [Fact]
    public void Create_WithValidRequest_ReturnsCreatedResult()
    {
        var repository = new FakePropertyRepository();
        var service = new CreatePropertyService(repository);
        var controller = new PropertiesController(service);

        var result = controller.Create(new CreatePropertyRequest("Sample Property", "123 Main St"));

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(201, createdResult.StatusCode);
    }

    private sealed class FakePropertyRepository : IPropertyRepository
    {
        public Task<Property?> GetByIdAsync(PropertyId id, CancellationToken cancellationToken = default)
            => Task.FromResult<Property?>(null);

        public Task<IReadOnlyList<Property>> ListAsync(CancellationToken cancellationToken = default)
            => Task.FromResult<IReadOnlyList<Property>>(Array.Empty<Property>());

        public Task AddAsync(Property aggregate, CancellationToken cancellationToken = default)
            => Task.CompletedTask;

        public Task UpdateAsync(Property aggregate, CancellationToken cancellationToken = default)
            => Task.CompletedTask;

        public Task DeleteAsync(Property aggregate, CancellationToken cancellationToken = default)
            => Task.CompletedTask;
    }
}
