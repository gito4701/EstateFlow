using EstateFlow.Domain.Owners;

namespace EstateFlow.Application.Responses;

public sealed record GetOwnerResponse(bool IsSuccess, Owner? Owner = null, string? Error = null, OwnerId? OwnerId = null) : ApplicationResponse;
