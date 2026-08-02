using EstateFlow.Domain.Tenants;

namespace EstateFlow.Application.Responses;

public sealed record CreateTenantResponse(bool IsSuccess, Tenant? Tenant = null, string? Error = null, TenantId? TenantId = null) : ApplicationResponse;
