using System.Linq;
using EstateFlow.Application.Requests;
using EstateFlow.Application.Services;
using EstateFlow.Domain.Owners;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace EstateFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class OwnersController : ApiControllerBase
{
    private readonly CreateOwnerService _createOwnerService;
    private readonly GetOwnerService _getOwnerService;

    public OwnersController(CreateOwnerService createOwnerService, GetOwnerService getOwnerService)
    {
        _createOwnerService = createOwnerService;
        _getOwnerService = getOwnerService;
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateOwnerRequest request)
    {
        var response = _createOwnerService.Handle(request);

        if (!response.IsSuccess || response.OwnerId is null)
        {
            return BuildErrorResponse(StatusCodes.Status400BadRequest, "Unable to create owner.", response.Error ?? "Unable to create owner.");
        }

        return CreatedAtAction(nameof(GetById), new { id = response.OwnerId.Value }, new
        {
            id = response.OwnerId.Value,
            name = response.Owner?.Name
        });
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var response = _getOwnerService.Handle(new GetOwnerQuery(new OwnerId(id)));

        if (!response.IsSuccess || response.Owner is null)
        {
            return BuildErrorResponse(StatusCodes.Status404NotFound, "Owner not found.", response.Error ?? "Owner not found.");
        }

        return Ok(new
        {
            id = response.Owner.Id.Value,
            name = response.Owner.Name
        });
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var owners = _getOwnerService.HandleAll();

        return Ok(owners.Select(owner => new
        {
            id = owner.Id.Value,
            name = owner.Name
        }));
    }

}
