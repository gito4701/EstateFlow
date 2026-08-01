using EstateFlow.Application.Persistence;
using EstateFlow.Application.Requests;
using EstateFlow.Application.Responses;
using EstateFlow.Domain.Properties;

namespace EstateFlow.Application.Services;

public sealed class GetPropertyService : ApplicationServiceBase
{
    private readonly IPropertyRepository _propertyRepository;

    public GetPropertyService(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    public GetPropertyResponse Handle(GetPropertyQuery query)
    {
        var property = _propertyRepository.GetByIdAsync(query.PropertyId).GetAwaiter().GetResult();

        if (property is null)
        {
            return new GetPropertyResponse(false, null, "Property not found.", query.PropertyId);
        }

        return new GetPropertyResponse(true, property, null, property.Id);
    }

    public IReadOnlyList<Property> HandleAll()
    {
        return _propertyRepository.GetAllAsync().GetAwaiter().GetResult();
    }
}
