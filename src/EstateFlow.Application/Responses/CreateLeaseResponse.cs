using EstateFlow.Domain.Leases;

namespace EstateFlow.Application.Responses;

public sealed record CreateLeaseResponse(bool IsSuccess, Lease? Lease = null, string? Error = null, LeaseId? LeaseId = null) : ApplicationResponse;
