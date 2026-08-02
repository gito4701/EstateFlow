using EstateFlow.Domain.Tenants;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EstateFlow.Application.Persistence;

public interface ITenantRepository
{
    Task AddAsync(Tenant aggregate, CancellationToken cancellationToken = default);
    Task<Tenant?> GetByIdAsync(TenantId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Tenant>> ListAsync(CancellationToken cancellationToken = default);
}
