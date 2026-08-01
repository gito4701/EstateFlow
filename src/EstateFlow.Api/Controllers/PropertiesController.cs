using EstateFlow.Application.Requests;
using EstateFlow.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EstateFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class PropertiesController : ControllerBase
{
    private readonly CreatePropertyService _createPropertyService;
    private readonly GetPropertyService _getPropertyService;

    public PropertiesController(CreatePropertyService createPropertyService, GetPropertyService getPropertyService)
    {
        _createPropertyService = createPropertyService;
        _getPropertyService = getPropertyService;
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

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var response = _getPropertyService.Handle(new GetPropertyQuery(new EstateFlow.Domain.Properties.PropertyId(id)));

        if (!response.IsSuccess || response.Property is null)
        {
            return NotFound(new { message = response.Error ?? "Property not found." });
        }

        return Ok(new
        {
            id = response.Property.Id.Value,
            name = response.Property.Name,
            address = response.Property.Address,
            state = response.Property.State.ToString()
        });
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var properties = _getPropertyService.HandleAll();

        return Ok(properties.Select(property => new
        {
            id = property.Id.Value,
            name = property.Name,
            address = property.Address,
            state = property.State.ToString()
        }));
    }
}
