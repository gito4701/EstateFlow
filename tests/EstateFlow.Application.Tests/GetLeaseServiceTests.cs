using System.Threading;
using System.Threading.Tasks;
using EstateFlow.Application.Persistence;
using EstateFlow.Application.Requests;
using EstateFlow.Application.Responses;
using EstateFlow.Application.Services;
using EstateFlow.Domain.Leases;
using Xunit;

namespace EstateFlow.Application.Tests;

public class GetLeaseServiceTests
{
    [Fact]
    public void Handle_WithExistingLease_ReturnsSuccessResponse()
    {
        var lease = Lease.Create(LeaseId.NewId(), "Existing Lease");
        var repository = new FakeLeaseRepository(lease);
        var service = new GetLeaseService(repository);

        var response = service.Handle(new GetLeaseQuery(lease.Id));

        Assert.NotNull(response);
        Assert.True(response.IsSuccess);
        Assert.NotNull(response.Lease);
        Assert.Equal(lease.Id, response.LeaseId);
        Assert.Equal(lease.Name, response.Lease!.Name);
    }

    [Fact]
    public void Handle_WithMissingLease_ReturnsFailureResponse()
    {
        var repository = new FakeLeaseRepository();
        var service = new GetLeaseService(repository);
        var missingId = LeaseId.NewId();

        var response = service.Handle(new GetLeaseQuery(missingId));

        Assert.NotNull(response);
        Assert.False(response.IsSuccess);
        Assert.Null(response.Lease);
        Assert.Equal(missingId, response.LeaseId);
        Assert.Contains("Lease not found", response.Error);
    }

    private sealed class FakeLeaseRepository : ILeaseRepository
    {
        private readonly Lease? _lease;

        public FakeLeaseRepository(Lease? lease = null)
        {
            _lease = lease;
        }

        public Task AddAsync(Lease aggregate, CancellationToken cancellationToken = default)
            => Task.CompletedTask;

        public Task<IReadOnlyList<Lease>> ListAsync(CancellationToken cancellationToken = default)
            => Task.FromResult<IReadOnlyList<Lease>>(Array.Empty<Lease>());

        public Task<Lease?> GetByIdAsync(LeaseId id, CancellationToken cancellationToken = default)
            => Task.FromResult(_lease != null && _lease.Id.Equals(id) ? _lease : null);
    }
}
