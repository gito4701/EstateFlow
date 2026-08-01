using EstateFlow.Domain.Properties;

namespace EstateFlow.Application.Responses;

public sealed record CreatePropertyResponse(bool IsSuccess, Property? Property = null, string? Error = null) : ApplicationResponse;
