using EstateFlow.Application.Requests;
using EstateFlow.Application.Responses;
using EstateFlow.Domain.Exceptions;
using EstateFlow.Domain.Properties;

namespace EstateFlow.Application.Services;

public sealed class CreatePropertyService : ApplicationServiceBase
{
    public CreatePropertyResponse Handle(CreatePropertyRequest request)
    {
        try
        {
            var property = Property.Create(PropertyId.NewId(), request.Name, request.Address);
            return new CreatePropertyResponse(true, property);
        }
        catch (InvalidPropertyException ex)
        {
            return new CreatePropertyResponse(false, null, ex.Message);
        }
    }
}
