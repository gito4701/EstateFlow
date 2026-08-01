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

public class DeletePropertyServiceTests
{
    [Fact]
    public void Handle_WithExistingProperty_ArchivesPropertyAndReturnsSuccessResponse()
    {
        var repository = new FakePropertyRepository();
        var property = Property.Create(PropertyId.NewId(), "Sample Property", "123 Main St");
        repository.StoredProperty = property;
        var service = new DeletePropertyService(repository);

        var response = service.Handle(new DeletePropertyRequest(property.Id));

        Assert.True(response.IsSuccess);
        Assert.NotNull(response.Property);
        Assert.Equal(property.Id, response.Property.Id);
        Assert.Equal(PropertyLifecycleState.Archived, response.Property.State);
        Assert.Same(property, repository.UpdatedProperty);
    }

    [Fact]
    public void Handle_WithMissingProperty_ReturnsFailureResponse()
    {
        var repository = new FakePropertyRepository();
        var service = new DeletePropertyService(repository);

        var response = service.Handle(new DeletePropertyRequest(PropertyId.NewId()));

        Assert.False(response.IsSuccess);
        Assert.NotNull(response.Error);
        Assert.Null(response.Property);
        Assert.False(response.PropertyId.HasValue);
        Assert.Null(repository.UpdatedProperty);
    }

    [Fact]
    public void Handle_WithAlreadyArchivedProperty_ReturnsFailureResponse()
    {
        var repository = new FakePropertyRepository();
        var property = Property.Create(PropertyId.NewId(), "Sample Property", "123 Main St");
        property.Delete();
        repository.StoredProperty = property;
        var service = new DeletePropertyService(repository);

        var response = service.Handle(new DeletePropertyRequest(property.Id));

        Assert.False(response.IsSuccess);
        Assert.NotNull(response.Error);
        Assert.Null(repository.UpdatedProperty);
    }

    private sealed class FakePropertyRepository : IPropertyRepository
    {
        public Property? StoredProperty { get; set; }
        public Property? UpdatedProperty { get; private set; }

        public Task<Property?> GetByIdAsync(PropertyId id, CancellationToken cancellationToken = default)
            => Task.FromResult(StoredProperty);

        public Task<IReadOnlyList<Property>> GetAllAsync(CancellationToken cancellationToken = default)
            => Task.FromResult<IReadOnlyList<Property>>(Array.Empty<Property>());

        public Task<IReadOnlyList<Property>> ListAsync(CancellationToken cancellationToken = default)
            => Task.FromResult<IReadOnlyList<Property>>(Array.Empty<Property>());

        public Task AddAsync(Property aggregate, CancellationToken cancellationToken = default)
        {
            StoredProperty = aggregate;
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Property aggregate, CancellationToken cancellationToken = default)
        {
            UpdatedProperty = aggregate;
            StoredProperty = aggregate;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Property aggregate, CancellationToken cancellationToken = default)
            => Task.CompletedTask;
    }
}
