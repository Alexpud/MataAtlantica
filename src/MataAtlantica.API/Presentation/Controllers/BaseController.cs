using FluentResults;
using MataAtlantica.API.Presentation.Models;
using Microsoft.AspNetCore.Mvc;

namespace MataAtlantica.API.Presentation.Controllers;

[ApiController]
[Produces("application/json")]
public abstract class BaseController : ControllerBase
{
    public IActionResult HandleFailedResult<T>(Result<T> result)
    {
        var badRequestResponse = new BadRequestResponse();
        foreach(var error in result.Errors)
            badRequestResponse.AddError(error);

        badRequestResponse.Message = "Alguns erros foram encontrados";
        return BadRequest(badRequestResponse);
    }

    public IActionResult HandleFailedResult(Result result)
    {
        var badRequestResponse = new BadRequestResponse();
        foreach (var error in result.Errors)
            badRequestResponse.AddError(error);

        badRequestResponse.Message = "Alguns erros foram encontrados";
        return BadRequest(badRequestResponse);
    }
}
