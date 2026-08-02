using System.Threading;
using System.Threading.Tasks;
using EstateFlow.Domain.Leases;

namespace EstateFlow.Application.Persistence;

public interface ILeaseRepository
{
    Task AddAsync(Lease aggregate, CancellationToken cancellationToken = default);
}
