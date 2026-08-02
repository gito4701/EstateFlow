using System;
using System.Linq;
using System.Threading.Tasks;
using EstateFlow.Domain.Properties;
using EstateFlow.Infrastructure.Persistence;
using EstateFlow.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EstateFlow.Infrastructure.Tests;

public sealed class PropertyAuditRepositoryTests
{
    [Fact]
    public async Task AddAsync_WithNewProperty_CreatesCreateAuditEntry()
    {
        await using var context = CreateContext();
        var repository = new PropertyRepository(context);
        var property = Property.Create(PropertyId.NewId(), "Audit Property", "123 Audit St");

        await repository.AddAsync(property);

        var auditEntries = await context.PropertyAuditEntries
            .Where(entry => entry.PropertyId.Value == property.Id.Value)
            .ToListAsync();

        var auditEntry = Assert.Single(auditEntries);
        Assert.Equal(PropertyAuditOperation.Create, auditEntry.Operation);
        Assert.Equal(property.Id.Value, auditEntry.PropertyId.Value);
    }

    [Fact]
    public async Task UpdateAsync_WithUpdatedProperty_CreatesUpdateAuditEntry()
    {
        await using var context = CreateContext();
        var repository = new PropertyRepository(context);
        var property = Property.Create(PropertyId.NewId(), "Original", "123 Main St");

        await repository.AddAsync(property);

        property.Update("Updated", "456 Main St");
        await repository.UpdateAsync(property);

        var auditEntries = await context.PropertyAuditEntries
            .Where(entry => entry.PropertyId.Value == property.Id.Value)
            .OrderBy(entry => entry.OccurredAtUtc)
            .ToListAsync();

        Assert.Equal(2, auditEntries.Count);
        Assert.Equal(PropertyAuditOperation.Create, auditEntries[0].Operation);
        Assert.Equal(PropertyAuditOperation.Update, auditEntries[1].Operation);
    }

    [Fact]
    public async Task DeleteAsync_WithArchivedProperty_CreatesDeleteAuditEntry()
    {
        await using var context = CreateContext();
        var property = Property.Create(PropertyId.NewId(), "Delete Me", "789 Main St");
        await context.Properties.AddAsync(property);
        await context.SaveChangesAsync();

        var repository = new PropertyRepository(context);
        property.Delete();

        await repository.DeleteAsync(property);

        var auditEntries = await context.PropertyAuditEntries
            .Where(entry => entry.PropertyId.Value == property.Id.Value)
            .ToListAsync();

        var auditEntry = Assert.Single(auditEntries);
        Assert.Equal(PropertyAuditOperation.Delete, auditEntry.Operation);
        Assert.Equal(property.Id.Value, auditEntry.PropertyId.Value);
    }

    private static EstateFlowDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<EstateFlowDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new EstateFlowDbContext(options);
    }
}
