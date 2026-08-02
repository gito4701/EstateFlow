using EstateFlow.Domain.Leases;

namespace EstateFlow.Application.Requests;

public sealed record GetLeaseQuery(LeaseId LeaseId) : ApplicationRequest;
