using FluentResults;
using MataAtlantica.Infrastructure.Services;
using MediatR;

namespace MataAtlantica.Application.Usuarios.Login;

public record LoginCommand(string Login, string Senha) : IRequest<Result> { }

public class CommandHandler(UserIdentityService userIdentityService) : IRequestHandler<LoginCommand, Result>
{
    private readonly UserIdentityService _userIdentityService = userIdentityService;
    
    public async Task<Result> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = await _userIdentityService.Login(request.Login, request.Senha);
        return result.Succeeded ? Result.Ok() : Result.Fail("Login falhou");
    }
}
