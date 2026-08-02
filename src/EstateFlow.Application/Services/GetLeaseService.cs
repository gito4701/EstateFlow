using System.Collections.Generic;
using EstateFlow.Application.Persistence;
using EstateFlow.Application.Requests;
using EstateFlow.Application.Responses;
using EstateFlow.Domain.Leases;

namespace EstateFlow.Application.Services;

public sealed class GetLeaseService : ApplicationServiceBase
{
    private readonly ILeaseRepository _leaseRepository;

    public GetLeaseService(ILeaseRepository leaseRepository)
    {
        _leaseRepository = leaseRepository;
    }

    public GetLeaseResponse Handle(GetLeaseQuery query)
    {
        var lease = _leaseRepository.GetByIdAsync(query.LeaseId).GetAwaiter().GetResult();

        if (lease is null)
        {
            return new GetLeaseResponse(false, null, "Lease not found.", query.LeaseId);
        }

        return new GetLeaseResponse(true, lease, null, lease.Id);
    }

    public IReadOnlyList<Lease> HandleAll()
    {
        return _leaseRepository.ListAsync().GetAwaiter().GetResult();
    }
}
