using EstateFlow.Application.Persistence;
using EstateFlow.Application.Requests;
using EstateFlow.Application.Responses;
using EstateFlow.Domain.Properties;

namespace EstateFlow.Application.Services;

public sealed class GetPropertyAuditService : ApplicationServiceBase
{
    private readonly IPropertyRepository _propertyRepository;

    public GetPropertyAuditService(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    public GetPropertyAuditResponse Handle(GetPropertyAuditQuery query)
    {
        var entries = _propertyRepository.GetPropertyAuditEntriesAsync(query.PropertyId).GetAwaiter().GetResult();

        if (entries.Count > 0)
        {
            return new GetPropertyAuditResponse(true, entries, null, query.PropertyId);
        }

        var property = _propertyRepository.GetByIdAsync(query.PropertyId).GetAwaiter().GetResult();

        if (property is null)
        {
            return new GetPropertyAuditResponse(false, null, "Property not found.", query.PropertyId);
        }

        return new GetPropertyAuditResponse(true, Array.Empty<PropertyAuditEntry>(), null, query.PropertyId);
    }
}
