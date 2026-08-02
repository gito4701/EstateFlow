using EstateFlow.Application.Persistence;
using EstateFlow.Application.Requests;
using EstateFlow.Application.Responses;
using EstateFlow.Domain.Exceptions;
using EstateFlow.Domain.Owners;

namespace EstateFlow.Application.Services;

public sealed class CreateOwnerService : ApplicationServiceBase
{
    private readonly IOwnerRepository _ownerRepository;

    public CreateOwnerService(IOwnerRepository ownerRepository)
    {
        _ownerRepository = ownerRepository;
    }

    public CreateOwnerResponse Handle(CreateOwnerRequest request)
    {
        return ExecuteWithDomainException<InvalidOwnerException, CreateOwnerResponse>(
            () =>
            {
                var owner = Owner.Create(OwnerId.NewId(), request.Name);
                _ownerRepository.AddAsync(owner).GetAwaiter().GetResult();
                return new CreateOwnerResponse(true, owner, null, owner.Id);
            },
            ex => new CreateOwnerResponse(false, null, ex.Message, null));
    }
}
