using EstateFlow.Application.Requests;
using EstateFlow.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EstateFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class PropertiesController : ControllerBase
{
    private readonly CreatePropertyService _createPropertyService;

    public PropertiesController(CreatePropertyService createPropertyService)
    {
        _createPropertyService = createPropertyService;
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreatePropertyRequest request)
    {
        var response = _createPropertyService.Handle(request);

        if (!response.IsSuccess || response.PropertyId is null)
        {
            return BadRequest(new { message = response.Error ?? "Unable to create property." });
        }

        return CreatedAtAction(nameof(Create), new { id = response.PropertyId.Value }, new
        {
            id = response.PropertyId.Value,
            name = response.Property?.Name,
            address = response.Property?.Address
        });
    }
}
