using EstateFlow.Domain.Properties;

namespace EstateFlow.Application.Responses;

public sealed record DeletePropertyResponse(bool IsSuccess, Property? Property = null, string? Error = null, PropertyId? PropertyId = null) : ApplicationResponse;
