using EstateFlow.Application.Persistence;
using EstateFlow.Domain.Owners;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EstateFlow.Infrastructure.Persistence.Abstractions;

public interface IOwnerRepository : Application.Persistence.IOwnerRepository
{
    Task<Owner?> GetByIdAsync(OwnerId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Owner>> ListAsync(CancellationToken cancellationToken = default);
}
