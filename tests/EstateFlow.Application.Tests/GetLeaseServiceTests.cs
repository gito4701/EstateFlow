using System;
using System.Collections.Generic;
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

    [Fact]
    public void HandleAll_ReturnsAllLeasesFromRepository()
    {
        var firstLease = Lease.Create(LeaseId.NewId(), "First Lease");
        var secondLease = Lease.Create(LeaseId.NewId(), "Second Lease");
        var repository = new FakeLeaseRepository(firstLease, secondLease);
        var service = new GetLeaseService(repository);

        var leases = service.HandleAll();

        Assert.NotNull(leases);
        Assert.Equal(2, leases.Count);
        Assert.Contains(leases, lease => lease.Id.Equals(firstLease.Id));
        Assert.Contains(leases, lease => lease.Id.Equals(secondLease.Id));
    }

    private sealed class FakeLeaseRepository : ILeaseRepository
    {
        private readonly IReadOnlyList<Lease> _leases;

        public FakeLeaseRepository(params Lease[] leases)
        {
            _leases = leases;
        }

        public Task AddAsync(Lease aggregate, CancellationToken cancellationToken = default)
            => Task.CompletedTask;

        public Task<IReadOnlyList<Lease>> ListAsync(CancellationToken cancellationToken = default)
            => Task.FromResult(_leases);

        public Task<Lease?> GetByIdAsync(LeaseId id, CancellationToken cancellationToken = default)
            => Task.FromResult(_leases.FirstOrDefault(lease => lease.Id.Equals(id)));
    }
}
