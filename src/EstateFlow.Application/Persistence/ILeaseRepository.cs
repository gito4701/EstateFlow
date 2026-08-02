using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EstateFlow.Domain.Leases;

namespace EstateFlow.Application.Persistence;

public interface ILeaseRepository
{
    Task<Lease?> GetByIdAsync(LeaseId id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Lease>> ListAsync(CancellationToken cancellationToken = default);

    Task AddAsync(Lease aggregate, CancellationToken cancellationToken = default);
}
