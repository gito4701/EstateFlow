using System.ComponentModel.DataAnnotations;
using EstateFlow.Domain.Properties;

namespace EstateFlow.Application.Requests;

public sealed record UpdatePropertyRequest(PropertyId PropertyId, [Required(AllowEmptyStrings = false)] string Name, [Required(AllowEmptyStrings = false)] string Address) : ApplicationRequest;
