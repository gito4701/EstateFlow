using System;
using System.Threading.Tasks;
using EstateFlow.Domain.Properties;
using EstateFlow.Infrastructure.Persistence;
using EstateFlow.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EstateFlow.Infrastructure.Tests;

public class PropertyRepositoryDeleteTests
{
    [Fact]
    public async Task UpdateAsync_WithArchivedProperty_PersistsArchivedState()
    {
        await using var context = CreateContext();
        var property = Property.Create(PropertyId.NewId(), "Sample Property", "123 Main St");
        await context.Properties.AddAsync(property);
        await context.SaveChangesAsync();

        var repository = new PropertyRepository(context);
        property.Delete();
        await repository.UpdateAsync(property);

        var result = await repository.GetByIdAsync(property.Id);

        Assert.NotNull(result);
        Assert.Equal(PropertyLifecycleState.Archived, result!.State);
    }

    private static EstateFlowDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<EstateFlowDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new EstateFlowDbContext(options);
    }
}
