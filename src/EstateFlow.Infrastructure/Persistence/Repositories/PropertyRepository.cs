using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EstateFlow.Application.Persistence;
using EstateFlow.Domain.Properties;
using EstateFlow.Infrastructure.Persistence.Abstractions;
using EstateFlow.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EstateFlow.Infrastructure.Persistence.Repositories;

public sealed class PropertyRepository : PersistenceRepositoryBase<Property, PropertyId>, EstateFlow.Infrastructure.Persistence.Abstractions.IPropertyRepository
{
    public PropertyRepository(EstateFlowDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<Property?> GetByIdAsync(PropertyId id, CancellationToken cancellationToken = default)
    {
        return await Entities.OfType<Property>().FirstOrDefaultAsync(property => property.Id.Equals(id), cancellationToken);
    }

    public async Task<IReadOnlyList<Property>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await Entities.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Property aggregate, CancellationToken cancellationToken = default)
    {
        _dbContext.Properties.Update(aggregate);
        await _dbContext.PropertyAuditEntries.AddAsync(PropertyAuditEntry.Create(aggregate.Id, PropertyAuditOperation.Update), cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Property aggregate, CancellationToken cancellationToken = default)
    {
        _dbContext.Properties.Remove(aggregate);
        await _dbContext.PropertyAuditEntries.AddAsync(PropertyAuditEntry.Create(aggregate.Id, PropertyAuditOperation.Delete), cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public override async Task AddAsync(Property aggregate, CancellationToken cancellationToken = default)
    {
        await _dbContext.Properties.AddAsync(aggregate, cancellationToken);
        await _dbContext.PropertyAuditEntries.AddAsync(PropertyAuditEntry.Create(aggregate.Id, PropertyAuditOperation.Create), cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<PropertyAuditEntry>> GetPropertyAuditEntriesAsync(PropertyId id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.PropertyAuditEntries
            .Where(entry => entry.PropertyId.Value == id.Value)
            .OrderBy(entry => entry.OccurredAtUtc)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<SearchPropertiesResult> SearchAsync(string? name, PropertyLifecycleState? status, int page, int pageSize, string? sortField, string? sortDirection, CancellationToken cancellationToken = default)
    {
        IQueryable<Property> query = Entities.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(property => property.Name.ToLower().Contains(name.ToLower()));
        }

        if (status.HasValue)
        {
            query = query.Where(property => property.State == status.Value);
        }

        var totalCount = await query.CountAsync(cancellationToken);

        if (!string.IsNullOrWhiteSpace(sortField))
        {
            query = sortField.Equals("name", StringComparison.OrdinalIgnoreCase)
                ? (sortDirection?.Equals("desc", StringComparison.OrdinalIgnoreCase) == true
                    ? query.OrderByDescending(property => property.Name)
                    : query.OrderBy(property => property.Name))
                : (sortDirection?.Equals("desc", StringComparison.OrdinalIgnoreCase) == true
                    ? query.OrderByDescending(property => property.State)
                    : query.OrderBy(property => property.State));
        }
        else
        {
            query = query.OrderBy(property => property.Name);
        }

        var properties = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        return new SearchPropertiesResult(properties, page, pageSize, totalCount);
    }
}
