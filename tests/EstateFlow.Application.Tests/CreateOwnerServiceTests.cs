using System.Threading;
using System.Threading.Tasks;
using EstateFlow.Application.Persistence;
using EstateFlow.Application.Requests;
using EstateFlow.Application.Responses;
using EstateFlow.Application.Services;
using EstateFlow.Domain.Owners;
using Xunit;

namespace EstateFlow.Application.Tests;

public class CreateOwnerServiceTests
{
    [Fact]
    public void Handle_WithValidRequest_PersistsOwnerAndReturnsSuccessResponse()
    {
        var repository = new FakeOwnerRepository();
        var service = new CreateOwnerService(repository);
        var request = new CreateOwnerRequest("Sample Owner");

        var response = service.Handle(request);

        Assert.NotNull(response);
        Assert.True(response.IsSuccess);
        Assert.NotNull(response.Owner);
        Assert.True(response.OwnerId.HasValue);
        Assert.Equal(response.Owner.Id, response.OwnerId.Value);
        Assert.Same(response.Owner, repository.AddedOwner);
        Assert.Equal("Sample Owner", response.Owner.Name);
    }

    [Fact]
    public void Handle_WithInvalidRequest_ReturnsFailureResponseWithoutPersisting()
    {
        var repository = new FakeOwnerRepository();
        var service = new CreateOwnerService(repository);
        var request = new CreateOwnerRequest("");

        var response = service.Handle(request);

        Assert.NotNull(response);
        Assert.False(response.IsSuccess);
        Assert.NotNull(response.Error);
        Assert.Null(response.Owner);
        Assert.False(response.OwnerId.HasValue);
        Assert.Null(repository.AddedOwner);
    }

    private sealed class FakeOwnerRepository : IOwnerRepository
    {
        public Owner? AddedOwner { get; private set; }

        public Task AddAsync(Owner aggregate, CancellationToken cancellationToken = default)
        {
            AddedOwner = aggregate;
            return Task.CompletedTask;
        }
    }
}
