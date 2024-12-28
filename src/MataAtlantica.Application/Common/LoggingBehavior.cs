using MataAtlantica.Utils;
using MediatR;

namespace MataAtlantica.Application.Common;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogService _logger;

    public LoggingBehavior(ILogService logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Message={Message}; HandledType={HandledType}; Request={Request}", 
            "Handling mediatr request", 
            typeof(TRequest).Name,
            request);
        var response = await next();
        //_logger.LogInformation($"Handled {typeof(TResponse).Name}");

        return response;
    }
}