using System.Threading;
using System.Threading.Tasks;
using EstateFlow.Domain.Tenants;
using EstateFlow.Infrastructure.Persistence.Abstractions;
using EstateFlow.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EstateFlow.Infrastructure.Persistence.Repositories;

public sealed class TenantRepository : PersistenceRepositoryBase<Tenant, TenantId>, EstateFlow.Infrastructure.Persistence.Abstractions.ITenantRepository
{
    public TenantRepository(EstateFlowDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<Tenant?> GetByIdAsync(TenantId id, CancellationToken cancellationToken = default)
    {
        return await Entities.OfType<Tenant>().FirstOrDefaultAsync(tenant => tenant.Id.Equals(id), cancellationToken);
    }
}
