using System.Threading;
using System.Threading.Tasks;
using EstateFlow.Application.Persistence;
using EstateFlow.Application.Requests;
using EstateFlow.Application.Responses;
using EstateFlow.Application.Services;
using EstateFlow.Domain.Leases;
using Xunit;

namespace EstateFlow.Application.Tests;

public class CreateLeaseServiceTests
{
    [Fact]
    public void Handle_WithValidRequest_PersistsLeaseAndReturnsSuccessResponse()
    {
        var repository = new FakeLeaseRepository();
        var service = new CreateLeaseService(repository);
        var request = new CreateLeaseRequest("Sample Lease");

        var response = service.Handle(request);

        Assert.NotNull(response);
        Assert.True(response.IsSuccess);
        Assert.NotNull(response.Lease);
        Assert.True(response.LeaseId.HasValue);
        Assert.Equal(response.Lease.Id, response.LeaseId.Value);
        Assert.Same(response.Lease, repository.AddedLease);
        Assert.Equal("Sample Lease", response.Lease.Name);
    }

    [Fact]
    public void Handle_WithInvalidRequest_ReturnsFailureResponseWithoutPersisting()
    {
        var repository = new FakeLeaseRepository();
        var service = new CreateLeaseService(repository);
        var request = new CreateLeaseRequest(string.Empty);

        var response = service.Handle(request);

        Assert.NotNull(response);
        Assert.False(response.IsSuccess);
        Assert.NotNull(response.Error);
        Assert.Null(response.Lease);
        Assert.False(response.LeaseId.HasValue);
        Assert.Null(repository.AddedLease);
    }

    private sealed class FakeLeaseRepository : ILeaseRepository
    {
        public Lease? AddedLease { get; private set; }

        public Task AddAsync(Lease aggregate, CancellationToken cancellationToken = default)
        {
            AddedLease = aggregate;
            return Task.CompletedTask;
        }
    }
}
