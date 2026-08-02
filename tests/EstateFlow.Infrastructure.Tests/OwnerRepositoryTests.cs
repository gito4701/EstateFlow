using System.Threading.Tasks;
using EstateFlow.Domain.Owners;
using EstateFlow.Infrastructure.Persistence;
using EstateFlow.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EstateFlow.Infrastructure.Tests;

public class OwnerRepositoryTests
{
    [Fact]
    public async Task AddAsync_PersistsOwnerToDatabase()
    {
        await using var context = CreateContext();
        var repository = new OwnerRepository(context);
        var owner = Owner.Create(OwnerId.NewId(), "Sample Owner");

        await repository.AddAsync(owner);

        var persisted = await context.Owners.SingleAsync(item => item.Id.Equals(owner.Id));

        Assert.NotNull(persisted);
        Assert.Equal(owner.Id, persisted.Id);
        Assert.Equal("Sample Owner", persisted.Name);
    }

    [Fact]
    public async Task OwnerId_Conversion_PreservesIdentifierAfterSave()
    {
        await using var context = CreateContext();
        var owner = Owner.Create(OwnerId.NewId(), "Conversion Owner");
        await context.Owners.AddAsync(owner);
        await context.SaveChangesAsync();

        var found = await context.Owners.FirstOrDefaultAsync(item => item.Id.Equals(owner.Id));

        Assert.NotNull(found);
        Assert.Equal(owner.Id, found!.Id);
        Assert.Equal("Conversion Owner", found.Name);
    }

    private static EstateFlowDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<EstateFlowDbContext>()
            .UseInMemoryDatabase(System.Guid.NewGuid().ToString())
            .Options;

        return new EstateFlowDbContext(options);
    }
}
