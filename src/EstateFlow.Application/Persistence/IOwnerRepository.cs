using EstateFlow.Domain.Owners;
using System.Threading;
using System.Threading.Tasks;

namespace EstateFlow.Application.Persistence;

public interface IOwnerRepository
{
    Task AddAsync(Owner aggregate, CancellationToken cancellationToken = default);
}
