using EstateFlow.Application.Persistence;
using EstateFlow.Application.Requests;
using EstateFlow.Application.Responses;
using EstateFlow.Domain.Exceptions;
using EstateFlow.Domain.Properties;

namespace EstateFlow.Application.Services;

public sealed class UpdatePropertyService : ApplicationServiceBase
{
    private readonly IPropertyRepository _propertyRepository;

    public UpdatePropertyService(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    public UpdatePropertyResponse Handle(UpdatePropertyRequest request)
    {
        try
        {
            var property = _propertyRepository.GetByIdAsync(request.PropertyId).GetAwaiter().GetResult();

            if (property is null)
            {
                return new UpdatePropertyResponse(false, null, "Property not found.", request.PropertyId);
            }

            property.Update(request.Name, request.Address);
            _propertyRepository.UpdateAsync(property).GetAwaiter().GetResult();

            return new UpdatePropertyResponse(true, property, null, property.Id);
        }
        catch (InvalidPropertyException ex)
        {
            return new UpdatePropertyResponse(false, null, ex.Message, request.PropertyId);
        }
        catch (ArgumentException ex)
        {
            return new UpdatePropertyResponse(false, null, ex.Message, request.PropertyId);
        }
        catch (InvalidOperationException ex)
        {
            return new UpdatePropertyResponse(false, null, ex.Message, request.PropertyId);
        }
    }
}
