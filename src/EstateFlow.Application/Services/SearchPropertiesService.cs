using EstateFlow.Application.Persistence;
using EstateFlow.Application.Requests;
using EstateFlow.Application.Responses;
using EstateFlow.Domain.Properties;

namespace EstateFlow.Application.Services;

public sealed class SearchPropertiesService : ApplicationServiceBase
{
    private readonly IPropertyRepository _propertyRepository;

    public SearchPropertiesService(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    public SearchPropertiesResponse Handle(SearchPropertiesRequest request)
    {
        if (request.Page < 1 || request.PageSize < 1)
        {
            return new SearchPropertiesResponse(false, Array.Empty<Property>(), 1, 10, 0, "Page and page size must be greater than zero.");
        }

        PropertyLifecycleState? status = null;
        if (!string.IsNullOrWhiteSpace(request.Status) && Enum.TryParse<PropertyLifecycleState>(request.Status, true, out var parsedStatus))
        {
            status = parsedStatus;
        }
        else if (!string.IsNullOrWhiteSpace(request.Status))
        {
            return new SearchPropertiesResponse(false, Array.Empty<Property>(), request.Page, request.PageSize, 0, "Invalid lifecycle status.");
        }

        var result = _propertyRepository.SearchAsync(request.Name, status, request.Page, request.PageSize, request.Sort, request.Direction).GetAwaiter().GetResult();

        return new SearchPropertiesResponse(true, result.Properties, result.Page, result.PageSize, result.TotalCount);
    }
}
