using System.Threading;
using System.Threading.Tasks;

namespace EstateFlow.Infrastructure.Persistence.Abstractions;

public interface IPersistenceService
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
