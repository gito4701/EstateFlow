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

public class CreatePropertyServiceTests
{
    [Fact]
    public void Handle_WithValidRequest_PersistsPropertyAndReturnsSuccessResponse()
    {
        var repository = new FakePropertyRepository();
        var service = new CreatePropertyService(repository);
        var request = new CreatePropertyRequest("Sample Property", "123 Main St");

        var response = service.Handle(request);

        Assert.NotNull(response);
        Assert.True(response.IsSuccess);
        Assert.NotNull(response.Property);
        Assert.True(response.PropertyId.HasValue);
        Assert.Equal(response.Property.Id, response.PropertyId.Value);
        Assert.Same(response.Property, repository.AddedProperty);
        Assert.Equal("Sample Property", response.Property.Name);
        Assert.Equal("123 Main St", response.Property.Address);
    }

    [Fact]
    public void Handle_WithInvalidRequest_ReturnsFailureResponseWithoutPersisting()
    {
        var repository = new FakePropertyRepository();
        var service = new CreatePropertyService(repository);
        var request = new CreatePropertyRequest("", "");

        var response = service.Handle(request);

        Assert.NotNull(response);
        Assert.False(response.IsSuccess);
        Assert.NotNull(response.Error);
        Assert.Null(response.Property);
        Assert.False(response.PropertyId.HasValue);
        Assert.Null(repository.AddedProperty);
    }

    private sealed class FakePropertyRepository : IPropertyRepository
    {
        public Property? AddedProperty { get; private set; }

        public Task<Property?> GetByIdAsync(PropertyId id, CancellationToken cancellationToken = default)
            => Task.FromResult<Property?>(null);

        public Task<IReadOnlyList<Property>> ListAsync(CancellationToken cancellationToken = default)
            => Task.FromResult<IReadOnlyList<Property>>(Array.Empty<Property>());

        public Task AddAsync(Property aggregate, CancellationToken cancellationToken = default)
        {
            AddedProperty = aggregate;
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Property aggregate, CancellationToken cancellationToken = default)
            => Task.CompletedTask;

        public Task DeleteAsync(Property aggregate, CancellationToken cancellationToken = default)
            => Task.CompletedTask;
    }
}
