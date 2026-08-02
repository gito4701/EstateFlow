using EstateFlow.Infrastructure.Common;
using EstateFlow.Infrastructure.Persistence;
using EstateFlow.Infrastructure.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EstateFlow.Infrastructure.Repositories;

public abstract class PersistenceRepositoryBase<TEntity, TId> : InfrastructureService
    where TEntity : class
{
    protected readonly EstateFlowDbContext _dbContext;

    protected PersistenceRepositoryBase(EstateFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected DbSet<TEntity> Entities => _dbContext.Set<TEntity>();

    public virtual async Task<IReadOnlyList<TEntity>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await Entities.AsNoTracking().ToListAsync(cancellationToken);
    }

    public virtual async Task AddAsync(TEntity aggregate, CancellationToken cancellationToken = default)
    {
        await Entities.AddAsync(aggregate, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
