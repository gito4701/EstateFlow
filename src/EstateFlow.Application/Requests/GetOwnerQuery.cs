using EstateFlow.Domain.Owners;

namespace EstateFlow.Application.Requests;

public sealed record GetOwnerQuery(OwnerId OwnerId) : ApplicationRequest;
