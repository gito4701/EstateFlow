using EstateFlow.Domain.Properties;

namespace EstateFlow.Application.Requests;

public sealed record GetPropertyQuery(PropertyId PropertyId) : ApplicationRequest;
