using FluentResults;
using MataAtlantica.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace MataAtlantica.API.Controllers;

[ApiController]
[Produces("application/json")]
public abstract class BaseController : ControllerBase
{
    protected IActionResult HandleResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
            return Ok(result.Value);
        return HandleFailedResult(result);
    }

    protected IActionResult HandleFailedResult<T>(Result<T> result)
    {
        var badRequestResponse = new BadRequestResponse();
        foreach (var error in result.Errors)
            badRequestResponse.AddError(error);

        badRequestResponse.Message = "Alguns erros foram encontrados";
        return BadRequest(badRequestResponse);
    }

    protected IActionResult HandleFailedResult(Result result)
    {
        var badRequestResponse = new BadRequestResponse();
        foreach (var error in result.Errors)
            badRequestResponse.AddError(error);

        badRequestResponse.Message = "Alguns erros foram encontrados";
        return BadRequest(badRequestResponse);
    }
}
