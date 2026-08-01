using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EstateFlow.Infrastructure.Persistence.Abstractions;

public interface IRepository<TAggregate, in TId>
    where TAggregate : class
{
    Task<TAggregate?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TAggregate>> ListAsync(CancellationToken cancellationToken = default);

    Task AddAsync(TAggregate aggregate, CancellationToken cancellationToken = default);

    Task UpdateAsync(TAggregate aggregate, CancellationToken cancellationToken = default);

    Task DeleteAsync(TAggregate aggregate, CancellationToken cancellationToken = default);
}
