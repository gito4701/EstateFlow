using System.Collections.Generic;
using EstateFlow.Application.Persistence;
using EstateFlow.Application.Requests;
using EstateFlow.Application.Responses;
using EstateFlow.Domain.Owners;

namespace EstateFlow.Application.Services;

public sealed class GetOwnerService : ApplicationServiceBase
{
    private readonly IOwnerRepository _ownerRepository;

    public GetOwnerService(IOwnerRepository ownerRepository)
    {
        _ownerRepository = ownerRepository;
    }

    public GetOwnerResponse Handle(GetOwnerQuery query)
    {
        var owner = _ownerRepository.GetByIdAsync(query.OwnerId).GetAwaiter().GetResult();

        if (owner is null)
        {
            return new GetOwnerResponse(false, null, "Owner not found.", query.OwnerId);
        }

        return new GetOwnerResponse(true, owner, null, owner.Id);
    }

    public IReadOnlyList<Owner> HandleAll()
    {
        return _ownerRepository.ListAsync().GetAwaiter().GetResult();
    }
}
