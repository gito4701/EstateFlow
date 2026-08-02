using System.ComponentModel.DataAnnotations;
using EstateFlow.Application.Requests;
using EstateFlow.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace EstateFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class PropertiesController : ApiControllerBase
{
    private readonly CreatePropertyService _createPropertyService;
    private readonly GetPropertyService _getPropertyService;
    private readonly UpdatePropertyService _updatePropertyService;
    private readonly DeletePropertyService _deletePropertyService;
    private readonly SearchPropertiesService _searchPropertiesService;

    public PropertiesController(CreatePropertyService createPropertyService, GetPropertyService getPropertyService, UpdatePropertyService updatePropertyService, DeletePropertyService deletePropertyService, SearchPropertiesService searchPropertiesService)
    {
        _createPropertyService = createPropertyService;
        _getPropertyService = getPropertyService;
        _updatePropertyService = updatePropertyService;
        _deletePropertyService = deletePropertyService;
        _searchPropertiesService = searchPropertiesService;
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreatePropertyRequest request)
    {
        var response = _createPropertyService.Handle(request);

        if (!response.IsSuccess || response.PropertyId is null)
        {
            return BuildErrorResponse(StatusCodes.Status400BadRequest, "Unable to create property.", response.Error ?? "Unable to create property.");
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
            return BuildErrorResponse(StatusCodes.Status404NotFound, "Property not found.", response.Error ?? "Property not found.");
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

    [HttpGet("search")]
    public IActionResult Search([FromQuery] string? name, [FromQuery] string? status, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? sort = null, [FromQuery] string? direction = null)
    {
        if (page < 1 || pageSize < 1)
        {
            return BuildErrorResponse(StatusCodes.Status400BadRequest, "One or more validation errors occurred.", "The request contains invalid data.");
        }

        var response = _searchPropertiesService.Handle(new SearchPropertiesRequest(name, status, page, pageSize, sort, direction));

        if (!response.IsSuccess)
        {
            return BuildErrorResponse(StatusCodes.Status400BadRequest, "Unable to search properties.", response.Error ?? "Unable to search properties.");
        }

        return Ok(new
        {
            page = response.Page,
            pageSize = response.PageSize,
            totalCount = response.TotalCount,
            items = response.Properties.Select(property => new
            {
                id = property.Id.Value,
                name = property.Name,
                address = property.Address,
                state = property.State.ToString()
            })
        });
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, [FromBody] UpdatePropertyRequest request)
    {
        if (request is null)
        {
            return BuildErrorResponse(StatusCodes.Status400BadRequest, "Request body is required.", "Request body is required.");
        }

        try
        {
            var response = _updatePropertyService.Handle(new UpdatePropertyRequest(new EstateFlow.Domain.Properties.PropertyId(id), request.Name, request.Address));

            if (!response.IsSuccess || response.Property is null)
            {
                if (response.Error?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true)
                {
                    return BuildErrorResponse(StatusCodes.Status404NotFound, "Property not found.", response.Error);
                }

                return BuildErrorResponse(StatusCodes.Status400BadRequest, "Unable to update property.", response.Error ?? "Unable to update property.");
            }

            return Ok(new
            {
                id = response.Property.Id.Value,
                name = response.Property.Name,
                address = response.Property.Address,
                state = response.Property.State.ToString()
            });
        }
        catch (Exception ex)
        {
            return BuildErrorResponse(StatusCodes.Status400BadRequest, "Unable to update property.", ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var response = _deletePropertyService.Handle(new DeletePropertyRequest(new EstateFlow.Domain.Properties.PropertyId(id)));

        if (!response.IsSuccess)
        {
            if (response.Error?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true)
            {
                return BuildErrorResponse(StatusCodes.Status404NotFound, "Property not found.", response.Error);
            }

            return BuildErrorResponse(StatusCodes.Status400BadRequest, "Unable to delete property.", response.Error ?? "Unable to delete property.");
        }

        return NoContent();
    }

}
