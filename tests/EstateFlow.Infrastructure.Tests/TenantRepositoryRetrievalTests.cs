using System.Threading.Tasks;
using EstateFlow.Domain.Tenants;
using EstateFlow.Infrastructure.Persistence;
using EstateFlow.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EstateFlow.Infrastructure.Tests;

public class TenantRepositoryRetrievalTests
{
    [Fact]
    public async Task GetByIdAsync_ReturnsPersistedTenant()
    {
        await using var context = CreateContext();
        var tenant = Tenant.Create(TenantId.NewId(), "Find Tenant");
        await context.Tenants.AddAsync(tenant);
        await context.SaveChangesAsync();

        var repository = new TenantRepository(context);
        var result = await repository.GetByIdAsync(tenant.Id);

        Assert.NotNull(result);
        Assert.Equal(tenant.Id, result!.Id);
        Assert.Equal("Find Tenant", result.Name);
    }

    private static EstateFlowDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<EstateFlowDbContext>()
            .UseInMemoryDatabase(System.Guid.NewGuid().ToString())
            .Options;

        return new EstateFlowDbContext(options);
    }
}
