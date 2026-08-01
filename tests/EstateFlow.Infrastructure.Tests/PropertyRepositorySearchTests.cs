using System;
using System.Linq;
using System.Threading.Tasks;
using EstateFlow.Domain.Properties;
using EstateFlow.Infrastructure.Persistence;
using EstateFlow.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EstateFlow.Infrastructure.Tests;

public class PropertyRepositorySearchTests
{
    [Fact]
    public async Task SearchAsync_WithNoFilters_ReturnsAllProperties()
    {
        await using var context = CreateContext();
        await context.Properties.AddRangeAsync(
            Property.Create(PropertyId.NewId(), "Alpha", "Address 1"),
            Property.Create(PropertyId.NewId(), "Beta", "Address 2"));
        await context.SaveChangesAsync();

        var repository = new PropertyRepository(context);

        var result = await repository.SearchAsync(null, null, 1, 10, null, null);

        Assert.Equal(2, result.TotalCount);
        Assert.Equal(2, result.Properties.Count);
    }

    [Fact]
    public async Task SearchAsync_WithNameFilter_ReturnsMatchingProperties()
    {
        await using var context = CreateContext();
        await context.Properties.AddRangeAsync(
            Property.Create(PropertyId.NewId(), "Alpha", "Address 1"),
            Property.Create(PropertyId.NewId(), "Beta", "Address 2"));
        await context.SaveChangesAsync();

        var repository = new PropertyRepository(context);

        var result = await repository.SearchAsync("alp", null, 1, 10, null, null);

        Assert.Single(result.Properties);
        Assert.Equal("Alpha", result.Properties[0].Name);
    }

    [Fact]
    public async Task SearchAsync_WithStatusFilter_ReturnsMatchingProperties()
    {
        await using var context = CreateContext();
        var activeProperty = Property.Create(PropertyId.NewId(), "Alpha", "Address 1");
        activeProperty.Activate();
        var draftProperty = Property.Create(PropertyId.NewId(), "Beta", "Address 2");
        await context.Properties.AddRangeAsync(activeProperty, draftProperty);
        await context.SaveChangesAsync();

        var repository = new PropertyRepository(context);

        var result = await repository.SearchAsync(null, PropertyLifecycleState.Active, 1, 10, null, null);

        Assert.Single(result.Properties);
        Assert.Equal(PropertyLifecycleState.Active, result.Properties[0].State);
    }

    [Fact]
    public async Task SearchAsync_WithSortingAndPaging_ReturnsOrderedSlice()
    {
        await using var context = CreateContext();
        await context.Properties.AddRangeAsync(
            Property.Create(PropertyId.NewId(), "Zeta", "Address 3"),
            Property.Create(PropertyId.NewId(), "Alpha", "Address 1"),
            Property.Create(PropertyId.NewId(), "Beta", "Address 2"));
        await context.SaveChangesAsync();

        var repository = new PropertyRepository(context);

        var result = await repository.SearchAsync(null, null, 2, 1, "name", "asc");

        Assert.Single(result.Properties);
        Assert.Equal("Beta", result.Properties[0].Name);
        Assert.Equal(3, result.TotalCount);
        Assert.Equal(2, result.Page);
    }

    [Fact]
    public async Task SearchAsync_WithEmptyResult_ReturnsEmptyCollection()
    {
        await using var context = CreateContext();
        var repository = new PropertyRepository(context);

        var result = await repository.SearchAsync("missing", null, 1, 10, null, null);

        Assert.Empty(result.Properties);
        Assert.Equal(0, result.TotalCount);
    }

    private static EstateFlowDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<EstateFlowDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new EstateFlowDbContext(options);
    }
}
