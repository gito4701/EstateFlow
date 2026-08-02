using EstateFlow.Application.Persistence;
using EstateFlow.Application.Requests;
using EstateFlow.Application.Responses;
using EstateFlow.Domain.Exceptions;
using EstateFlow.Domain.Tenants;

namespace EstateFlow.Application.Services;

public sealed class CreateTenantService : ApplicationServiceBase
{
    private readonly ITenantRepository _tenantRepository;

    public CreateTenantService(ITenantRepository tenantRepository)
    {
        _tenantRepository = tenantRepository;
    }

    public CreateTenantResponse Handle(CreateTenantRequest request)
    {
        return ExecuteWithDomainException<InvalidTenantException, CreateTenantResponse>(
            () =>
            {
                var tenant = Tenant.Create(TenantId.NewId(), request.Name);
                _tenantRepository.AddAsync(tenant).GetAwaiter().GetResult();
                return new CreateTenantResponse(true, tenant, null, tenant.Id);
            },
            ex => new CreateTenantResponse(false, null, ex.Message, null));
    }
}
