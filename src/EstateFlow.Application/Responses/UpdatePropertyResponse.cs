using EstateFlow.Domain.Properties;

namespace EstateFlow.Application.Responses;

public sealed record UpdatePropertyResponse(bool IsSuccess, Property? Property = null, string? Error = null, PropertyId? PropertyId = null) : ApplicationResponse;
