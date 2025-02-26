using MediatR;

namespace MataAtlantica.Application.Common;

public class CommandHandlerExemplo : IRequestHandler<Requisicao, string>
{
    public Task<string> Handle(Requisicao request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}