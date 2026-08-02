using EstateFlow.Domain.Owners;
using EstateFlow.Infrastructure.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EstateFlow.Infrastructure.Persistence.Repositories;

public sealed class OwnerRepository : EstateFlow.Infrastructure.Persistence.Abstractions.IOwnerRepository
{
    private readonly EstateFlowDbContext _dbContext;

    public OwnerRepository(EstateFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Owner?> GetByIdAsync(OwnerId id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Owners.FirstOrDefaultAsync(owner => owner.Id.Equals(id), cancellationToken);
    }

    public async Task<IReadOnlyList<Owner>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Owners.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Owner aggregate, CancellationToken cancellationToken = default)
    {
        await _dbContext.Owners.AddAsync(aggregate, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
