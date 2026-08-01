using System;
using System.Threading.Tasks;
using EstateFlow.Domain.Properties;
using EstateFlow.Infrastructure.Persistence;
using EstateFlow.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EstateFlow.Infrastructure.Tests;

public class PropertyRepositoryUpdateTests
{
    [Fact]
    public async Task UpdateAsync_PersistsUpdatedProperty()
    {
        await using var context = CreateContext();
        var property = Property.Create(PropertyId.NewId(), "Original", "Original Address");
        await context.Properties.AddAsync(property);
        await context.SaveChangesAsync();

        var repository = new PropertyRepository(context);
        property.Update("Updated", "Updated Address");
        await repository.UpdateAsync(property);

        var persisted = await context.Properties.SingleAsync(item => item.Id.Equals(property.Id));

        Assert.Equal("Updated", persisted.Name);
        Assert.Equal("Updated Address", persisted.Address);
    }

    private static EstateFlowDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<EstateFlowDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new EstateFlowDbContext(options);
    }
}
