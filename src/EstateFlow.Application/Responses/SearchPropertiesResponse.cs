using System.Collections.Generic;
using EstateFlow.Domain.Properties;

namespace EstateFlow.Application.Responses;

public sealed record SearchPropertiesResponse(bool IsSuccess, IReadOnlyList<Property> Properties, int Page, int PageSize, int TotalCount, string? Error = null) : ApplicationResponse;
