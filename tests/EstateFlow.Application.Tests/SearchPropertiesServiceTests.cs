using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EstateFlow.Application.Persistence;
using EstateFlow.Application.Requests;
using EstateFlow.Application.Services;
using EstateFlow.Domain.Properties;
using Xunit;

namespace EstateFlow.Application.Tests;

public class SearchPropertiesServiceTests
{
    [Fact]
    public void Handle_WithDefaultRequest_ReturnsRepositoryResult()
    {
        var repository = new FakePropertyRepository();
        var service = new SearchPropertiesService(repository);
        var request = new SearchPropertiesRequest();

        var response = service.Handle(request);

        Assert.True(response.IsSuccess);
        Assert.NotNull(response.Properties);
        Assert.Equal(1, response.TotalCount);
        Assert.Equal(1, response.Page);
        Assert.Equal(10, response.PageSize);
    }

    [Fact]
    public void Handle_WithInvalidPaging_ReturnsFailureResponse()
    {
        var repository = new FakePropertyRepository();
        var service = new SearchPropertiesService(repository);
        var request = new SearchPropertiesRequest(name: null, status: null, page: 0, pageSize: 0, sort: null, direction: null);

        var response = service.Handle(request);

        Assert.False(response.IsSuccess);
        Assert.NotNull(response.Error);
        Assert.Empty(response.Properties);
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
        {
            var property = Property.Create(PropertyId.NewId(), "Sample Property", "123 Main St");
            property.Activate();
            return Task.FromResult(new SearchPropertiesResult(new[] { property }, page, pageSize, 1));
        }
    }
}
