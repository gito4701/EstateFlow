using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EstateFlow.Domain.Properties;
using EstateFlow.Infrastructure.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EstateFlow.Infrastructure.Persistence.Repositories;

public sealed class PropertyRepository : IPropertyRepository
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
}
