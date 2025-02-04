using FluentResults;
using MataAtlantica.Domain.Abstract.Repositories;
using MataAtlantica.Domain.Erros;
using MataAtlantica.Domain.Specifications;
using MataAtlantica.Infrastructure.Abstract;
using MataAtlantica.Infrastructure.Identity;
using MataAtlantica.Utils;
using Microsoft.AspNetCore.Identity;


namespace MataAtlantica.Infrastructure.Services;

public class UserIdentityService(
    IUsuarioRepository usuarioRepository,
    IUserManagerWrapper userManagerWrapper,
    IUserStore<User> userStore,
    ILogService logService,
     SignInManager<User> signInManager)
{
    private readonly IUsuarioRepository usuarioRepository = usuarioRepository;
    private readonly IUserManagerWrapper userManagerWrapper = userManagerWrapper;
    private readonly IUserStore<User> userStore = userStore;
    private readonly ILogService logService = logService;

    public async Task<Result<IdentityUser>> AdicionarUsuario(string login, string senha)
    {
        var usuario = await usuarioRepository.ObterPorSpec(new BuscarUsuarioPorLoginSpecification(login));
        if (usuario != null)
            return Result.Fail(BusinessErrors.UsuarioComLoginJaExiste);

        var user = new User();
        var emailStore = (IUserEmailStore<User>)userStore;
        await userStore.SetUserNameAsync(user, login, CancellationToken.None);
        await emailStore.SetEmailAsync(user, login, CancellationToken.None);
        var result = await userManagerWrapper.GetUserManager().CreateAsync(user, senha);

        if (!result.Succeeded)
        {
            var errors = string.Join(',', result.Errors.Select(p => p.Code));
            logService.LogError("Message={Message}; Login={Login}; ErrorCodes={ErrorCodes}",
                "A adição do usuário no identity falhou",
                login,
                errors);
            throw new Exception("Adição do usuário no sistema falhou");
        }
        return new IdentityUser(user.Id);
    }

    public async Task<SignInResult> Login(string login, string senha)
    {
        signInManager.AuthenticationScheme = IdentityConstants.BearerScheme;
        return await signInManager.PasswordSignInAsync(login, senha, false, lockoutOnFailure: true);
    }
}

public record struct IdentityUser(string Id);


