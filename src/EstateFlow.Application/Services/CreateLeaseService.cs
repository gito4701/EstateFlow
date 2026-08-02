using EstateFlow.Application.Persistence;
using EstateFlow.Application.Requests;
using EstateFlow.Application.Responses;
using EstateFlow.Domain.Exceptions;
using EstateFlow.Domain.Leases;

namespace EstateFlow.Application.Services;

public sealed class CreateLeaseService : ApplicationServiceBase
{
    private readonly ILeaseRepository _leaseRepository;

    public CreateLeaseService(ILeaseRepository leaseRepository)
    {
        _leaseRepository = leaseRepository;
    }

    public CreateLeaseResponse Handle(CreateLeaseRequest request)
    {
        return ExecuteWithDomainException<InvalidLeaseException, CreateLeaseResponse>(
            () =>
            {
                var lease = Lease.Create(LeaseId.NewId(), request.Name);
                _leaseRepository.AddAsync(lease).GetAwaiter().GetResult();
                return new CreateLeaseResponse(true, lease, null, lease.Id);
            },
            ex => new CreateLeaseResponse(false, null, ex.Message, null));
    }
}
