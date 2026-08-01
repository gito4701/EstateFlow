using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EstateFlow.Application.Persistence;
using EstateFlow.Application.Responses;
using EstateFlow.Application.Services;
using EstateFlow.Domain.Properties;
using Xunit;

namespace EstateFlow.Application.Tests;

public class GetPropertyServiceTests
{
    [Fact]
    public void Handle_WithExistingProperty_ReturnsSuccessResponse()
    {
        var property = Property.Create(PropertyId.NewId(), "Sample Property", "123 Main St");
        var repository = new FakePropertyRepository(property);
        var service = new GetPropertyService(repository);
        var query = new EstateFlow.Application.Requests.GetPropertyQuery(property.Id);

        var response = service.Handle(query);

        Assert.NotNull(response);
        Assert.True(response.IsSuccess);
        Assert.Same(property, response.Property);
        Assert.Equal(property.Id, response.PropertyId);
        Assert.Null(response.Error);
    }

    [Fact]
    public void Handle_WithMissingProperty_ReturnsFailureResponse()
    {
        var repository = new FakePropertyRepository(null);
        var service = new GetPropertyService(repository);
        var query = new EstateFlow.Application.Requests.GetPropertyQuery(PropertyId.NewId());

        var response = service.Handle(query);

        Assert.NotNull(response);
        Assert.False(response.IsSuccess);
        Assert.Null(response.Property);
        Assert.Equal(query.PropertyId, response.PropertyId);
        Assert.NotNull(response.Error);
    }

    [Fact]
    public void HandleAll_ReturnsAllPropertiesFromRepository()
    {
        var firstProperty = Property.Create(PropertyId.NewId(), "First", "First Address");
        var secondProperty = Property.Create(PropertyId.NewId(), "Second", "Second Address");
        var repository = new FakePropertyRepository(firstProperty, secondProperty);
        var service = new GetPropertyService(repository);

        var properties = service.HandleAll();

        Assert.NotNull(properties);
        Assert.Equal(2, properties.Count);
        Assert.Contains(properties, property => property.Id == firstProperty.Id);
        Assert.Contains(properties, property => property.Id == secondProperty.Id);
    }

    private sealed class FakePropertyRepository : IPropertyRepository
    {
        private readonly IReadOnlyList<Property> _properties;

        public FakePropertyRepository(params Property[] properties)
        {
            _properties = properties;
        }

        public Task<Property?> GetByIdAsync(PropertyId id, CancellationToken cancellationToken = default)
        {
            var property = _properties.FirstOrDefault(item => item.Id.Equals(id));
            return Task.FromResult(property);
        }

        public Task<IReadOnlyList<Property>> GetAllAsync(CancellationToken cancellationToken = default)
            => Task.FromResult(_properties);

        public Task<IReadOnlyList<Property>> ListAsync(CancellationToken cancellationToken = default)
            => Task.FromResult(_properties);

        public Task AddAsync(Property aggregate, CancellationToken cancellationToken = default)
            => Task.CompletedTask;

        public Task UpdateAsync(Property aggregate, CancellationToken cancellationToken = default)
            => Task.CompletedTask;

        public Task DeleteAsync(Property aggregate, CancellationToken cancellationToken = default)
            => Task.CompletedTask;
    }
}
