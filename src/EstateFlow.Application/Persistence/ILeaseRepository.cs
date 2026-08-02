using EstateFlow.Domain.Leases;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EstateFlow.Application.Persistence;

public interface ILeaseRepository
{
    Task AddAsync(Lease aggregate, CancellationToken cancellationToken = default);
    Task<Lease?> GetByIdAsync(LeaseId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Lease>> ListAsync(CancellationToken cancellationToken = default);
}
