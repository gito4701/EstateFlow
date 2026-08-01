using System.ComponentModel.DataAnnotations;

namespace EstateFlow.Application.Requests;

public sealed record CreatePropertyRequest([Required(AllowEmptyStrings = false)] string Name, [Required(AllowEmptyStrings = false)] string Address) : ApplicationRequest;
