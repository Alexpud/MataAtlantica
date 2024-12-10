
using MataAtlantica.Utils;

namespace MataAtlantica.API.Middleware;

public class RequestContextMiddleware
{
    private readonly RequestDelegate _next;

    public RequestContextMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context, RequestContextId requestContextId)
    {
        if (context.Request.Headers.ContainsKey("RequestContextId"))
            requestContextId.ContextId = context.Request.Headers["RequestContextId"];
        else
            requestContextId.ContextId = Guid.NewGuid().ToString();
        await _next.Invoke(context);
    }
}
