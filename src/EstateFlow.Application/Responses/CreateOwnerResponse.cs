using EstateFlow.Domain.Owners;

namespace EstateFlow.Application.Responses;

public sealed record CreateOwnerResponse(bool IsSuccess, Owner? Owner = null, string? Error = null, OwnerId? OwnerId = null) : ApplicationResponse;
