using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EstateFlow.Domain.Properties;

namespace EstateFlow.Application.Persistence;

public interface IPropertyRepository
{
    Task<Property?> GetByIdAsync(PropertyId id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Property>> ListAsync(CancellationToken cancellationToken = default);

    Task AddAsync(Property aggregate, CancellationToken cancellationToken = default);

    Task UpdateAsync(Property aggregate, CancellationToken cancellationToken = default);

    Task DeleteAsync(Property aggregate, CancellationToken cancellationToken = default);
}
