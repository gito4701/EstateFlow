using System.Collections.Generic;
using EstateFlow.Domain.Properties;

namespace EstateFlow.Application.Responses;

public sealed record GetPropertyAuditResponse(bool IsSuccess, IReadOnlyList<PropertyAuditEntry>? Entries = null, string? Error = null, PropertyId? PropertyId = null) : ApplicationResponse;
