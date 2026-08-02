using System.Linq;
using EstateFlow.Application.Requests;
using EstateFlow.Application.Services;
using EstateFlow.Domain.Leases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EstateFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class LeasesController : ApiControllerBase
{
    private readonly CreateLeaseService _createLeaseService;
    private readonly GetLeaseService _getLeaseService;

    public LeasesController(CreateLeaseService createLeaseService, GetLeaseService getLeaseService)
    {
        _createLeaseService = createLeaseService;
        _getLeaseService = getLeaseService;
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateLeaseRequest request)
    {
        var response = _createLeaseService.Handle(request);

        if (!response.IsSuccess || response.LeaseId is null)
        {
            return BuildErrorResponse(StatusCodes.Status400BadRequest, "Unable to create lease.", response.Error ?? "Unable to create lease.");
        }

        return CreatedAtAction(nameof(GetById), new { id = response.LeaseId.Value }, new
        {
            id = response.LeaseId.Value,
            name = response.Lease?.Name
        });
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var response = _getLeaseService.Handle(new GetLeaseQuery(new LeaseId(id)));

        if (!response.IsSuccess || response.Lease is null)
        {
            return BuildErrorResponse(StatusCodes.Status404NotFound, "Lease not found.", response.Error ?? "Lease not found.");
        }

        return Ok(new
        {
            id = response.Lease.Id.Value,
            name = response.Lease.Name
        });
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var leases = _getLeaseService.HandleAll();

        return Ok(leases.Select(lease => new
        {
            id = lease.Id.Value,
            name = lease.Name
        }));
    }
}
