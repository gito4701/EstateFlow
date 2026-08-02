using EstateFlow.Domain.Owners;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EstateFlow.Application.Persistence;

public interface IOwnerRepository
{
    Task AddAsync(Owner aggregate, CancellationToken cancellationToken = default);
    Task<Owner?> GetByIdAsync(OwnerId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Owner>> ListAsync(CancellationToken cancellationToken = default);
}
