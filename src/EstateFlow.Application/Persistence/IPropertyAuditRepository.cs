using System.Threading;
using System.Threading.Tasks;
using EstateFlow.Domain.Properties;

namespace EstateFlow.Application.Persistence;

public interface IPropertyAuditRepository
{
    Task AddAsync(PropertyAuditEntry entry, CancellationToken cancellationToken = default);
}
