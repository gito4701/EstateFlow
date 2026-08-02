using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EstateFlow.Application.Persistence;
using EstateFlow.Application.Requests;
using EstateFlow.Application.Responses;
using EstateFlow.Application.Services;
using EstateFlow.Domain.Properties;
using Xunit;

namespace EstateFlow.Application.Tests;

public class UpdatePropertyServiceTests
{
    [Fact]
    public void Handle_WithExistingProperty_UpdatesPropertyAndReturnsSuccessResponse()
    {
        var property = Property.Create(PropertyId.NewId(), "Original", "Original Address");
        var repository = new FakePropertyRepository(property);
        var service = new UpdatePropertyService(repository);
        var request = new UpdatePropertyRequest(property.Id, "Updated", "Updated Address");

        var response = service.Handle(request);

        Assert.NotNull(response);
        Assert.True(response.IsSuccess);
        Assert.NotNull(response.Property);
        Assert.Equal(property.Id, response.PropertyId);
        Assert.Equal("Updated", response.Property!.Name);
        Assert.Equal("Updated Address", response.Property.Address);
        Assert.Same(property, response.Property);
    }

    [Fact]
    public void Handle_WithMissingProperty_ReturnsFailureResponse()
    {
        var repository = new FakePropertyRepository();
        var service = new UpdatePropertyService(repository);
        var request = new UpdatePropertyRequest(PropertyId.NewId(), "Updated", "Updated Address");

        var response = service.Handle(request);

        Assert.NotNull(response);
        Assert.False(response.IsSuccess);
        Assert.Null(response.Property);
        Assert.Equal(request.PropertyId, response.PropertyId);
        Assert.NotNull(response.Error);
    }

    [Fact]
    public void Handle_WithInvalidUpdate_ReturnsFailureResponse()
    {
        var property = Property.Create(PropertyId.NewId(), "Original", "Original Address");
        var repository = new FakePropertyRepository(property);
        var service = new UpdatePropertyService(repository);
        var request = new UpdatePropertyRequest(property.Id, "", "");

        var response = service.Handle(request);

        Assert.NotNull(response);
        Assert.False(response.IsSuccess);
        Assert.Null(response.Property);
        Assert.Equal(property.Id, response.PropertyId);
        Assert.NotNull(response.Error);
    }

    private sealed class FakePropertyRepository : IPropertyRepository
    {
        private readonly Property? _property;

        public FakePropertyRepository(Property? property = null)
        {
            _property = property;
        }

        public Task<Property?> GetByIdAsync(PropertyId id, CancellationToken cancellationToken = default)
            => Task.FromResult(_property?.Id.Equals(id) == true ? _property : null);

        public Task<IReadOnlyList<Property>> GetAllAsync(CancellationToken cancellationToken = default)
            => Task.FromResult<IReadOnlyList<Property>>(Array.Empty<Property>());

        public Task<IReadOnlyList<Property>> ListAsync(CancellationToken cancellationToken = default)
            => Task.FromResult<IReadOnlyList<Property>>(Array.Empty<Property>());

        public Task<SearchPropertiesResult> SearchAsync(string? name, PropertyLifecycleState? status, int page, int pageSize, string? sortField, string? sortDirection, CancellationToken cancellationToken = default)
            => Task.FromResult(new SearchPropertiesResult(Array.Empty<Property>(), page, pageSize, 0));

        public Task AddAsync(Property aggregate, CancellationToken cancellationToken = default)
            => Task.CompletedTask;

        public Task UpdateAsync(Property aggregate, CancellationToken cancellationToken = default)
            => Task.CompletedTask;

        public Task DeleteAsync(Property aggregate, CancellationToken cancellationToken = default)
            => Task.CompletedTask;
    }
}
