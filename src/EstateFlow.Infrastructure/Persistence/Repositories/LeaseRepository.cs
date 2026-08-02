using System.Threading;
using System.Threading.Tasks;
using EstateFlow.Application.Persistence;
using EstateFlow.Domain.Leases;
using EstateFlow.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EstateFlow.Infrastructure.Persistence.Repositories;

public sealed class LeaseRepository : PersistenceRepositoryBase<Lease, LeaseId>, EstateFlow.Infrastructure.Persistence.Abstractions.ILeaseRepository
{
    public LeaseRepository(EstateFlowDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<Lease?> GetByIdAsync(LeaseId id, CancellationToken cancellationToken = default)
    {
        return await Entities.OfType<Lease>().FirstOrDefaultAsync(lease => lease.Id.Equals(id), cancellationToken);
    }
}
