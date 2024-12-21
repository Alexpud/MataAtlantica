using Microsoft.Extensions.Logging;
using Serilog;

namespace MataAtlantica.Utils;


public interface ILogService
{
    void LogInformation(string messageTemplate, params object[] args);
}

public class LogService(ILogger<LogService> logger, RequestContextId requestContextId) : ILogService
{
    private readonly string _contextId = requestContextId.ContextId;
    private readonly ILogger<LogService> _logger = logger;
    public void LogInformation(string messageTemplate, params object[] args)
    {
        var parametros = new List<object>() { _contextId.ToString() };
        parametros.AddRange(args);
        _logger.LogInformation("ContextId={ContextId}; " + messageTemplate, parametros.ToArray());
    }
}