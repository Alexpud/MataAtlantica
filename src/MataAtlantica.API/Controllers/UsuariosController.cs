using MataAtlantica.API.Models;
using MataAtlantica.Application.Usuarios.AdicionarUsuario;
using MataAtlantica.Domain.Models;
using MataAtlantica.Domain.Models.Usuarios;
using MataAtlantica.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace MataAtlantica.API.Controllers;

// Os endpoints foram baseados nos endpoints de identity da microsoft. FOnte: https://stackoverflow.com/questions/78049014/asp-net-core-8-web-api-editing-identity-endpoints
[Route("api/usuarios")]
public class UsuariosController(
    IMediator mediator,
    IUserStore<User> userStore,
    SignInManager<User> signInManager,
    UserManager<User> userManager) : BaseController
{
    private readonly SignInManager<User> signInManager = signInManager;
    private readonly UserManager<User> userManager = userManager;
    private readonly IUserStore<User> _userStore = userStore;
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
        if (result.IsSuccess)
            return Ok(result.Value);
        return HandleFailedResult(result);
    }



    ///// <summary>
    ///// Cria um novo usuário
    ///// </summary>
    ///// <param name="request"></param>
    ///// <returns></returns>
    //[HttpPost]
    //public async Task<IActionResult> AdicionarUsuario(AdicionarUsuarioRequest request)
    //{
    //    var email = request.Email;
    //    var user = new User();
    //    var emailStore = (IUserEmailStore<User>)userStore;
    //    await userStore.SetUserNameAsync(user, email, CancellationToken.None);
    //    await emailStore.SetEmailAsync(user, email, CancellationToken.None);
    //    var result = await userManager.CreateAsync(user, request.Password);
    //    if (!result.Succeeded)
    //        return BadRequest(result.Errors);
    //    return Ok();
    //}

    /// <summary>
    /// Executa o login e retorna o token de autenticação e refresh
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(Microsoft.AspNetCore.Identity.SignInResult), (int)HttpStatusCode.OK)]
    public async Task<IActionResult>  Login(LoginRequest request)
    {
        signInManager.AuthenticationScheme = IdentityConstants.BearerScheme;
        var result = await signInManager.PasswordSignInAsync(request.Email, request.Password, false, lockoutOnFailure: true);

        //if (result.RequiresTwoFactor)
        //{
        //    if (!string.IsNullOrEmpty(login.TwoFactorCode))
        //    {
        //        result = await signInManager.TwoFactorAuthenticatorSignInAsync(login.TwoFactorCode, isPersistent, rememberClient: isPersistent);
        //    }
        //    else if (!string.IsNullOrEmpty(login.TwoFactorRecoveryCode))
        //    {
        //        result = await signInManager.TwoFactorRecoveryCodeSignInAsync(login.TwoFactorRecoveryCode);
        //    }
        //}

        if (!result.Succeeded)
        {
            return Unauthorized();
        }

        // The signInManager already produced the needed response in the form of a cookie or bearer token.
        //return TypedResults.Empty;
        return Empty;
    }
}

public record LoginRequest(
    [Required(AllowEmptyStrings = false)][EmailAddress] string Email,
    [Required(AllowEmptyStrings = false)] string Password);

public record AdicionarUsuarioRequest(
    [Required(AllowEmptyStrings = false)][EmailAddress] string Login,
    [Required(AllowEmptyStrings = false)] string Nome,
    [Required(AllowEmptyStrings = false)] string Sobrenome,
    [Required(AllowEmptyStrings = false)] API.Models.Endereco Endereco,
    [Required(AllowEmptyStrings = false)] string Senha);
