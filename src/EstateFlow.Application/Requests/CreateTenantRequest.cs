using System.ComponentModel.DataAnnotations;

namespace EstateFlow.Application.Requests;

public sealed record CreateTenantRequest([Required(AllowEmptyStrings = false)] string Name) : ApplicationRequest;
