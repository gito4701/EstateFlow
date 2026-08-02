using System.Threading.Tasks;
using EstateFlow.Domain.Tenants;
using EstateFlow.Infrastructure.Persistence;
using EstateFlow.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EstateFlow.Infrastructure.Tests;

public class TenantRepositoryTests
{
    [Fact]
    public async Task AddAsync_PersistsTenantToDatabase()
    {
        await using var context = CreateContext();
        var repository = new TenantRepository(context);
        var tenant = Tenant.Create(TenantId.NewId(), "Sample Tenant");

        await repository.AddAsync(tenant);

        var persisted = await context.Tenants.SingleAsync(item => item.Id.Equals(tenant.Id));

        Assert.NotNull(persisted);
        Assert.Equal(tenant.Id, persisted.Id);
        Assert.Equal("Sample Tenant", persisted.Name);
    }

    [Fact]
    public async Task TenantId_Conversion_PreservesIdentifierAfterSave()
    {
        await using var context = CreateContext();
        var tenant = Tenant.Create(TenantId.NewId(), "Conversion Tenant");
        await context.Tenants.AddAsync(tenant);
        await context.SaveChangesAsync();

        var found = await context.Tenants.FirstOrDefaultAsync(item => item.Id.Equals(tenant.Id));

        Assert.NotNull(found);
        Assert.Equal(tenant.Id, found!.Id);
        Assert.Equal("Conversion Tenant", found.Name);
    }

    private static EstateFlowDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<EstateFlowDbContext>()
            .UseInMemoryDatabase(System.Guid.NewGuid().ToString())
            .Options;

        return new EstateFlowDbContext(options);
    }
}
