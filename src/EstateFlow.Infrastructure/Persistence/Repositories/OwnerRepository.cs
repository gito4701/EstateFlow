using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EstateFlow.Domain.Owners;
using EstateFlow.Infrastructure.Persistence.Abstractions;
using EstateFlow.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EstateFlow.Infrastructure.Persistence.Repositories;

public sealed class OwnerRepository : PersistenceRepositoryBase<Owner, OwnerId>, EstateFlow.Infrastructure.Persistence.Abstractions.IOwnerRepository
{
    public OwnerRepository(EstateFlowDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<Owner?> GetByIdAsync(OwnerId id, CancellationToken cancellationToken = default)
    {
        return await Entities.OfType<Owner>().FirstOrDefaultAsync(owner => owner.Id.Equals(id), cancellationToken);
    }
}
