using System.Threading;
using System.Threading.Tasks;
using EstateFlow.Domain.Properties;
using EstateFlow.Infrastructure.Persistence.Abstractions;
using EstateFlow.Infrastructure.Repositories;

namespace EstateFlow.Infrastructure.Persistence.Repositories;

public sealed class PropertyAuditRepository : PersistenceRepositoryBase<PropertyAuditEntry, Guid>, IPropertyAuditRepository
{
    public PropertyAuditRepository(EstateFlowDbContext dbContext)
        : base(dbContext)
    {
    }

    public override async Task AddAsync(PropertyAuditEntry entry, CancellationToken cancellationToken = default)
    {
        await Entities.AddAsync(entry, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
