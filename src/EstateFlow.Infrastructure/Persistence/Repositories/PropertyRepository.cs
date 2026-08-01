using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EstateFlow.Application.Persistence;
using EstateFlow.Domain.Properties;
using EstateFlow.Infrastructure.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EstateFlow.Infrastructure.Persistence.Repositories;

public sealed class PropertyRepository : EstateFlow.Infrastructure.Persistence.Abstractions.IPropertyRepository
{
    private readonly EstateFlowDbContext _dbContext;

    public PropertyRepository(EstateFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Property?> GetByIdAsync(PropertyId id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Properties.FirstOrDefaultAsync(property => property.Id.Equals(id), cancellationToken);
    }

    public async Task<IReadOnlyList<Property>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Properties.AsNoTracking().ToListAsync(cancellationToken);
    }

    public Task<IReadOnlyList<Property>> ListAsync(CancellationToken cancellationToken = default)
    {
        return GetAllAsync(cancellationToken);
    }

    public async Task AddAsync(Property aggregate, CancellationToken cancellationToken = default)
    {
        await _dbContext.Properties.AddAsync(aggregate, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Property aggregate, CancellationToken cancellationToken = default)
    {
        _dbContext.Properties.Update(aggregate);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Property aggregate, CancellationToken cancellationToken = default)
    {
        _dbContext.Properties.Remove(aggregate);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<SearchPropertiesResult> SearchAsync(string? name, PropertyLifecycleState? status, int page, int pageSize, string? sortField, string? sortDirection, CancellationToken cancellationToken = default)
    {
        IQueryable<Property> query = _dbContext.Properties.AsNoTracking();

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
