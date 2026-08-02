using EstateFlow.Domain.Tenants;

namespace EstateFlow.Application.Requests;

public sealed record GetTenantQuery(TenantId TenantId) : ApplicationRequest;
