using System;
using System.Threading.Tasks;
using EstateFlow.Domain.Leases;
using EstateFlow.Infrastructure.Persistence;
using EstateFlow.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EstateFlow.Infrastructure.Tests;

public class LeaseRepositoryRetrievalTests
{
    [Fact]
    public async Task GetByIdAsync_WithExistingLease_ReturnsLease()
    {
        await using var context = CreateContext();
        var lease = Lease.Create(LeaseId.NewId(), "Sample Lease");
        await context.Set<Lease>().AddAsync(lease);
        await context.SaveChangesAsync();

        var repository = new LeaseRepository(context);

        var result = await repository.GetByIdAsync(lease.Id);

        Assert.NotNull(result);
        Assert.Equal(lease.Id, result!.Id);
    }

    [Fact]
    public async Task ListAsync_ReturnsStoredLeases()
    {
        await using var context = CreateContext();
        await context.Set<Lease>().AddRangeAsync(
            Lease.Create(LeaseId.NewId(), "First Lease"),
            Lease.Create(LeaseId.NewId(), "Second Lease"));
        await context.SaveChangesAsync();

        var repository = new LeaseRepository(context);

        var result = await repository.ListAsync();

        Assert.Equal(2, result.Count);
    }

    private static EstateFlowDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<EstateFlowDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new EstateFlowDbContext(options);
    }
}
