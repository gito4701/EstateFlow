using EstateFlow.Domain.Properties;

namespace EstateFlow.Infrastructure.Persistence.Abstractions;

public interface IPropertyRepository : IRepository<Property, PropertyId>
{
}
