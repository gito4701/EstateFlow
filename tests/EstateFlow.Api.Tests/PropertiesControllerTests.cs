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
        var createService = new CreatePropertyService(repository);
        var getService = new GetPropertyService(repository);
        var updateService = new UpdatePropertyService(repository);
        var deleteService = new DeletePropertyService(repository);
        var searchService = new SearchPropertiesService(repository);
        var controller = new PropertiesController(createService, getService, updateService, deleteService, searchService);

        var result = controller.Create(new CreatePropertyRequest("Sample Property", "123 Main St"));

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(201, createdResult.StatusCode);
    }

    private sealed class FakePropertyRepository : IPropertyRepository
    {
        public Task<Property?> GetByIdAsync(PropertyId id, CancellationToken cancellationToken = default)
            => Task.FromResult<Property?>(null);

        public Task<IReadOnlyList<Property>> GetAllAsync(CancellationToken cancellationToken = default)
            => Task.FromResult<IReadOnlyList<Property>>(Array.Empty<Property>());

        public Task<IReadOnlyList<Property>> ListAsync(CancellationToken cancellationToken = default)
            => Task.FromResult<IReadOnlyList<Property>>(Array.Empty<Property>());

        public Task AddAsync(Property aggregate, CancellationToken cancellationToken = default)
            => Task.CompletedTask;

        public Task UpdateAsync(Property aggregate, CancellationToken cancellationToken = default)
            => Task.CompletedTask;

        public Task DeleteAsync(Property aggregate, CancellationToken cancellationToken = default)
            => Task.CompletedTask;

        public Task<SearchPropertiesResult> SearchAsync(string? name, PropertyLifecycleState? status, int page, int pageSize, string? sortField, string? sortDirection, CancellationToken cancellationToken = default)
            => Task.FromResult(new SearchPropertiesResult(Array.Empty<Property>(), page, pageSize, 0));
    }
}
