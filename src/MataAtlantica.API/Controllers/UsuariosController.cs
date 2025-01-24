using MataAtlantica.API.Models;
using MataAtlantica.API.Models.Usuarios;
using MataAtlantica.Application.Usuarios.AdicionarMetodoPagamento;
using MataAtlantica.Application.Usuarios.AdicionarUsuario;
using MataAtlantica.Application.Usuarios.AlterarMetodoPagamento;
using MataAtlantica.Application.Usuarios.Login;
using MataAtlantica.Domain.Models.Usuarios;
using MediatR;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MataAtlantica.API.Controllers;

// Os endpoints foram baseados nos endpoints de identity da microsoft. FOnte: https://stackoverflow.com/questions/78049014/asp-net-core-8-web-api-editing-identity-endpoints
[Route("api/usuarios")]
public class UsuariosController(IMediator mediator) : BaseController
{
    private readonly IMediator _mediator = mediator;

    /// <summary>
    /// Cria um novo usuário
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(UsuarioDto), (int) HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BadRequestResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> AdicionarUsuario(AdicionarUsuarioRequest request)
    {
        var command = new AdicionarUsuarioCommand(
            request.Login,
            request.Senha,
            request.Nome,
            request.Sobrenome,
            new Domain.Models.Endereco(
                request.Endereco.Rua, 
                request.Endereco.Bairro,
                request.Endereco.Numero, 
                request.Endereco.Cidade,
                request.Endereco.UF, 
                request.Endereco.CEP));
        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    /// <summary>
    /// Executa o login e retorna o token de autenticação e refresh
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(AccessTokenResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await _mediator.Send(new LoginCommand(request.Login, request.Senha));
        return result.IsFailed ? Unauthorized() : Empty;
    }

    /// <summary>
    /// Adiciona um método de pagamento a um usuário
    /// </summary>
    /// <param name="usuarioId">ID do usuário</param>
    /// <param name="request">Dados do método de pagamento</param>
    /// <returns></returns>
    [HttpPost("{usuarioId}/metodo-pagamento")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BadRequestResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> AdicionarMetodoPagamento(string usuarioId, AdicionarMetodoPagamentoRequest request)
    {
        var command = new AdicionarMetodoPagamentoCommand(usuarioId, request.Bandeira, request.NumeroIdentificacao, request.Validade, request.Tipo);
        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    /// <summary>
    /// Altera o metodo de pagamento do usuário
    /// </summary>
    /// <param name="usuarioId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{usuarioId}/metodo-pagamento")]
    public async Task<IActionResult> AlterarMetodoPagamento(string usuarioId, AlterarMetodoPagamentoRequest request)
    {
        var command = new AlterarMetodoPagamentoCommand(
            usuarioId, 
            request.MetodoPagamentoId, 
            request.Bandeira, 
            request.NumeroIdentificacao, 
            request.Validade, 
            request.Tipo);
        var result = await _mediator.Send(command);
        return HandleResult(result);
    }
}