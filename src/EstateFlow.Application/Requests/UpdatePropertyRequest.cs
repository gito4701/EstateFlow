using EstateFlow.Domain.Properties;

namespace EstateFlow.Application.Requests;

public sealed record UpdatePropertyRequest(PropertyId PropertyId, string Name, string Address) : ApplicationRequest;
