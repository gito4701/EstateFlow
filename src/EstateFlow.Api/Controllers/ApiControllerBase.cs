using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EstateFlow.Api.Controllers;

[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    protected IActionResult BuildErrorResponse(int statusCode, string title, string detail)
    {
        var problemDetails = new ProblemDetails
        {
            Type = "about:blank",
            Title = title,
            Detail = detail,
            Status = statusCode
        };

        return statusCode switch
        {
            StatusCodes.Status400BadRequest => BadRequest(problemDetails),
            StatusCodes.Status404NotFound => NotFound(problemDetails),
            _ => StatusCode(statusCode, problemDetails)
        };
    }
}
