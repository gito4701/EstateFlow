using EstateFlow.Application.Persistence;
using EstateFlow.Application.Requests;
using EstateFlow.Application.Responses;
using EstateFlow.Domain.Exceptions;
using EstateFlow.Domain.Properties;

namespace EstateFlow.Application.Services;

public sealed class CreatePropertyService : ApplicationServiceBase
{
    private readonly IPropertyRepository _propertyRepository;

    public CreatePropertyService(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    public CreatePropertyResponse Handle(CreatePropertyRequest request)
    {
        return ExecuteWithDomainException<InvalidPropertyException, CreatePropertyResponse>(
            () =>
            {
                var property = Property.Create(PropertyId.NewId(), request.Name, request.Address);
                _propertyRepository.AddAsync(property).GetAwaiter().GetResult();
                return new CreatePropertyResponse(true, property, null, property.Id);
            },
            ex => new CreatePropertyResponse(false, null, ex.Message, null));
    }
}
