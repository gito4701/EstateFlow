using EstateFlow.Domain.Properties;

namespace EstateFlow.Application.Requests;

public sealed record DeletePropertyRequest(PropertyId PropertyId) : ApplicationRequest;
