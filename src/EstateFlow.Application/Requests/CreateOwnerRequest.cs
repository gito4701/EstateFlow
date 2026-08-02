using System.ComponentModel.DataAnnotations;

namespace EstateFlow.Application.Requests;

public sealed record CreateOwnerRequest([Required(AllowEmptyStrings = false)] string Name) : ApplicationRequest;
