using MataAtlantica.Application.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MataAtlantica.API.Middleware;

public class ResultFilter : IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var objectResult = context.Result as ObjectResult;
        if (objectResult?.Value is BaseResponse)
        {
            context.Result = objectResult.Value switch
            {
                BaseResponse r when r.Errors.Any() => CreateResult(r),
                ICommandResponse command => new ObjectResult(command.GetValue()),
                CommandResponse command => new OkResult()
            };
        }

        await next();

        ObjectResult CreateResult(BaseResponse response)
        {
            return new ObjectResult("Errors")
            {
                StatusCode = StatusCodes.Status422UnprocessableEntity,
                Value = new { Title = "Alguns erros foram encontrados", Errors = response.Errors }
            };
        }
    }
}