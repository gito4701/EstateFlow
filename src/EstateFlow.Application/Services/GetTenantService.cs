using System.Collections.Generic;
using EstateFlow.Application.Persistence;
using EstateFlow.Application.Requests;
using EstateFlow.Application.Responses;
using EstateFlow.Domain.Tenants;

namespace EstateFlow.Application.Services;

public sealed class GetTenantService : ApplicationServiceBase
{
    private readonly ITenantRepository _tenantRepository;

    public GetTenantService(ITenantRepository tenantRepository)
    {
        _tenantRepository = tenantRepository;
    }

    public GetTenantResponse Handle(GetTenantQuery query)
    {
        var tenant = _tenantRepository.GetByIdAsync(query.TenantId).GetAwaiter().GetResult();

        if (tenant is null)
        {
            return new GetTenantResponse(false, null, "Tenant not found.", query.TenantId);
        }

        return new GetTenantResponse(true, tenant, null, tenant.Id);
    }

    public IReadOnlyList<Tenant> HandleAll()
    {
        return _tenantRepository.ListAsync().GetAwaiter().GetResult();
    }
}
