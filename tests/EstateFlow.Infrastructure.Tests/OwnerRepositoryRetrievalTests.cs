using System.Threading.Tasks;
using EstateFlow.Domain.Owners;
using EstateFlow.Infrastructure.Persistence;
using EstateFlow.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EstateFlow.Infrastructure.Tests;

public class OwnerRepositoryRetrievalTests
{
    [Fact]
    public async Task GetByIdAsync_ReturnsPersistedOwner()
    {
        await using var context = CreateContext();
        var owner = Owner.Create(OwnerId.NewId(), "Find Owner");
        await context.Owners.AddAsync(owner);
        await context.SaveChangesAsync();

        var repository = new OwnerRepository(context);
        var result = await repository.GetByIdAsync(owner.Id);

        Assert.NotNull(result);
        Assert.Equal(owner.Id, result!.Id);
        Assert.Equal("Find Owner", result.Name);
    }

    private static EstateFlowDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<EstateFlowDbContext>()
            .UseInMemoryDatabase(System.Guid.NewGuid().ToString())
            .Options;

        return new EstateFlowDbContext(options);
    }
}
