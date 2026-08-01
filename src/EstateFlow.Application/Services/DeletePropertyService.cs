using System;
using EstateFlow.Application.Persistence;
using EstateFlow.Application.Requests;
using EstateFlow.Application.Responses;
using EstateFlow.Domain.Exceptions;
using EstateFlow.Domain.Properties;

namespace EstateFlow.Application.Services;

public sealed class DeletePropertyService : ApplicationServiceBase
{
    private readonly IPropertyRepository _propertyRepository;

    public DeletePropertyService(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    public DeletePropertyResponse Handle(DeletePropertyRequest request)
    {
        try
        {
            var property = _propertyRepository.GetByIdAsync(request.PropertyId).GetAwaiter().GetResult();

            if (property is null)
            {
                return new DeletePropertyResponse(false, null, "Property not found.", request.PropertyId);
            }

            property.Delete();
            _propertyRepository.UpdateAsync(property).GetAwaiter().GetResult();

            return new DeletePropertyResponse(true, property, null, property.Id);
        }
        catch (Exception ex) when (ex is InvalidPropertyException || ex is InvalidPropertyStateException || ex is InvalidOperationException || ex is ArgumentException)
        {
            return new DeletePropertyResponse(false, null, ex.Message, request.PropertyId);
        }
    }
}
