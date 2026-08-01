using System;
using System.Linq;
using System.Threading.Tasks;
using EstateFlow.Domain.Properties;
using EstateFlow.Infrastructure.Persistence;
using EstateFlow.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EstateFlow.Infrastructure.Tests;

public class PropertyRepositoryTests
{
    [Fact]
    public async Task GetByIdAsync_WithExistingProperty_ReturnsProperty()
    {
        await using var context = CreateContext();
        var property = Property.Create(PropertyId.NewId(), "Sample", "123 Main St");
        await context.Properties.AddAsync(property);
        await context.SaveChangesAsync();

        var repository = new PropertyRepository(context);

        var result = await repository.GetByIdAsync(property.Id);

        Assert.NotNull(result);
        Assert.Equal(property.Id, result!.Id);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsStoredProperties()
    {
        await using var context = CreateContext();
        await context.Properties.AddRangeAsync(
            Property.Create(PropertyId.NewId(), "First", "Address 1"),
            Property.Create(PropertyId.NewId(), "Second", "Address 2"));
        await context.SaveChangesAsync();

        var repository = new PropertyRepository(context);

        var result = await repository.GetAllAsync();

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
