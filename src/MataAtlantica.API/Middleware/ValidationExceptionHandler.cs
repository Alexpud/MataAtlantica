using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace MataAtlantica.API.Middleware;

public class ValidationExceptionHandler(ILogger<ValidationExceptionHandler> logger) : IExceptionHandler
{
    private readonly ILogger<ValidationExceptionHandler> _logger = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not ValidationException)
            return false;
        await HandleExceptionAsync(httpContext, exception);
        return true;
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var response = new
        {
            title = "Alguns erros foram encontrados",
            status = HttpStatusCode.BadRequest,
            detail = "Erros de validação",
            errors = GetErrors(exception)
        };
        httpContext.Response.ContentType = "application/json";
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }


    private static IReadOnlyDictionary<string, string> GetErrors(Exception exception)
    {
        Dictionary<string, string> errors = null;
        if (exception is ValidationException validationException)
        {
            errors = validationException
                .Errors
                .ToDictionary(p => p.ErrorCode, p => p.ErrorMessage);
        }
        return errors;
    }
}