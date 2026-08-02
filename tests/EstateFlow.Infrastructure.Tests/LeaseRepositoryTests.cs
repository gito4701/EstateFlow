using System.Threading.Tasks;
using EstateFlow.Domain.Leases;
using EstateFlow.Infrastructure.Persistence;
using EstateFlow.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EstateFlow.Infrastructure.Tests;

public class LeaseRepositoryTests
{
    [Fact]
    public async Task AddAsync_PersistsLeaseToDatabase()
    {
        await using var context = CreateContext();
        var repository = new LeaseRepository(context);
        var lease = Lease.Create(LeaseId.NewId(), "Sample Lease");

        await repository.AddAsync(lease);

        var persisted = await context.Set<Lease>().SingleAsync(item => item.Id.Equals(lease.Id));

        Assert.NotNull(persisted);
        Assert.Equal(lease.Id, persisted.Id);
        Assert.Equal("Sample Lease", persisted.Name);
    }

    [Fact]
    public async Task LeaseId_Conversion_PreservesIdentifierAfterSave()
    {
        await using var context = CreateContext();
        var lease = Lease.Create(LeaseId.NewId(), "Conversion Lease");
        await context.Set<Lease>().AddAsync(lease);
        await context.SaveChangesAsync();

        var found = await context.Set<Lease>().FirstOrDefaultAsync(item => item.Id.Equals(lease.Id));

        Assert.NotNull(found);
        Assert.Equal(lease.Id, found!.Id);
        Assert.Equal("Conversion Lease", found.Name);
    }

    private static EstateFlowDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<EstateFlowDbContext>()
            .UseInMemoryDatabase(System.Guid.NewGuid().ToString())
            .Options;

        return new EstateFlowDbContext(options);
    }
}
