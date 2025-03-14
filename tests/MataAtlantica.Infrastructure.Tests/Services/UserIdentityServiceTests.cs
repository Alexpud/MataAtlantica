using AutoFixture;
using MataAtlantica.Domain.Abstract.Repositories;
using MataAtlantica.Domain.Erros;
using MataAtlantica.Domain.Specifications;
using MataAtlantica.Infrastructure.Abstract;
using MataAtlantica.Infrastructure.Identity;
using MataAtlantica.Infrastructure.Services;
using MataAtlantica.Infrastructure.Tests.Builder;
using MataAtlantica.Utils;
using Microsoft.AspNetCore.Identity;
using NSubstitute;

namespace MataAtlantica.Infrastructure.Tests.Services;

public class UserIdentityServiceTests
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IUserManagerWrapper _userManagerWrapper;
    private readonly IUserStore<User> _userStore;
    private readonly ILogService _logService;
    private readonly UserIdentityService _sut;
    private readonly Fixture _fixture = new();
    public UserIdentityServiceTests()
    {
        _usuarioRepository = Substitute.For<IUsuarioRepository>();
        _userManagerWrapper = Substitute.For<IUserManagerWrapper>();
        _userStore = Substitute.For<IUserStore<User>>();
        _logService = Substitute.For<ILogService>();
        _sut = new(_usuarioRepository, _userManagerWrapper, _userStore, _logService, signInManager: null);
    }

    [Trait("Feature", "Adicionar usuario")]
    [Fact(DisplayName = "Adicionar usuario no identity deve falhar quando usuario com mesmo email ja existir")]
    public async Task AdicionarUsuario_DeveFalhar_QuandoUsuarioComMesmoEmailJaExistir()
    {
        // Arrange
        _usuarioRepository.ObterPorSpec(Arg.Any<BuscarUsuarioPorLoginSpecification>())
            .Returns(new UsuarioBuilder().BuildDefault().Create());

        string login = _fixture.Create<string>();
        string password = _fixture.Create<string>();

        // Act
        var result = await _sut.AdicionarUsuario(login, password);

        // Assert
        Assert.Multiple(
            () => Assert.True(result.IsFailed),
            () => result.HasError(p => p.Message == BusinessErrors.UsuarioComLoginJaExiste.Message),
            () => result.HasError(p => (string)p.Metadata["ErrorCode"] == nameof(BusinessErrors.UsuarioComLoginJaExiste)));
    }
}
