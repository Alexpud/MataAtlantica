using FluentResults;
using MataAtlantica.Domain.Models;
using MataAtlantica.Domain.Models.Usuarios;
using MataAtlantica.Domain.Services;
using MataAtlantica.Infrastructure.Services;
using MediatR;

namespace MataAtlantica.Application.Usuarios.AdicionarUsuario;

public record AdicionarUsuarioCommand(
    string Login,
    string Senha,
    string Nome,
    string Sobrenome,
    Endereco Endereco) : IRequest<Result<UsuarioDto>>
{ }

public class CommandHandler(
    UserIdentityService userIdentityService,
    UsuarioService usuarioService) : IRequestHandler<AdicionarUsuarioCommand, Result<UsuarioDto>>
{
    private readonly UserIdentityService userIdentityService = userIdentityService;
    private readonly UsuarioService usuarioService = usuarioService;

    public async Task<Result<UsuarioDto>> Handle(AdicionarUsuarioCommand request, CancellationToken cancellationToken)
    {
        // Adicionar o usuario no identity
        var result = await userIdentityService.AdicionarUsuario(request.Login, request.Senha);
        if (result.IsFailed)
            return Result.Fail(result.Errors);

        // Adicionar o usuario no dominio
        var dto = new AdicionarUsuarioDto(result.Value.Id,
            request.Nome,
            request.Sobrenome,
            request.Login,
            request.Endereco);
        var resultDomain = await usuarioService.Adicionar(dto);
        if (resultDomain.IsFailed)
            return Result.Fail(resultDomain.Errors);
        return resultDomain;
    }
}
