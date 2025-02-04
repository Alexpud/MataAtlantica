using Microsoft.Extensions.Logging;

namespace MataAtlantica.Utils;


public interface ILogService
{
    void LogError(string messageTemplate, params object[] args);
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

    public void LogError(string messageTemplate, params object[] args)
    {
        var parametros = new List<object>() { _contextId.ToString() };
        parametros.AddRange(args);
        _logger.LogError("ContextId={ContextId}; " + messageTemplate, parametros.ToArray());
    }
}