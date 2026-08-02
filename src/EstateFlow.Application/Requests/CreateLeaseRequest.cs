using System.ComponentModel.DataAnnotations;

namespace EstateFlow.Application.Requests;

public sealed record CreateLeaseRequest([Required(AllowEmptyStrings = false)] string Name) : ApplicationRequest;
