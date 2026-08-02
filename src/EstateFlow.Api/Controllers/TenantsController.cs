using System.Linq;
using EstateFlow.Application.Requests;
using EstateFlow.Application.Services;
using EstateFlow.Domain.Tenants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EstateFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TenantsController : ApiControllerBase
{
    private readonly CreateTenantService _createTenantService;
    private readonly GetTenantService _getTenantService;

    public TenantsController(CreateTenantService createTenantService, GetTenantService getTenantService)
    {
        _createTenantService = createTenantService;
        _getTenantService = getTenantService;
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateTenantRequest request)
    {
        var response = _createTenantService.Handle(request);

        if (!response.IsSuccess || response.TenantId is null)
        {
            return BuildErrorResponse(StatusCodes.Status400BadRequest, "Unable to create tenant.", response.Error ?? "Unable to create tenant.");
        }

        return CreatedAtAction(nameof(GetById), new { id = response.TenantId.Value }, new
        {
            id = response.TenantId.Value,
            name = response.Tenant?.Name
        });
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var response = _getTenantService.Handle(new GetTenantQuery(new TenantId(id)));

        if (!response.IsSuccess || response.Tenant is null)
        {
            return BuildErrorResponse(StatusCodes.Status404NotFound, "Tenant not found.", response.Error ?? "Tenant not found.");
        }

        return Ok(new
        {
            id = response.Tenant.Id.Value,
            name = response.Tenant.Name
        });
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var tenants = _getTenantService.HandleAll();

        return Ok(tenants.Select(tenant => new
        {
            id = tenant.Id.Value,
            name = tenant.Name
        }));
    }
}
