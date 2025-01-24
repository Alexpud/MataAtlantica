using AutoFixture;
using FluentValidation.TestHelper;
using MataAtlantica.Domain.Abstract.Repositories;
using MataAtlantica.Domain.Entidades;
using MataAtlantica.Domain.Models.Usuarios;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Linq.Expressions;

namespace MataAtlantica.Domain.Tests.Models;

public class AlterarMetodoPagamentoDtoValidatorTests
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly Fixture _fixture = new();
    private readonly AlterarMetodoPagamentoDtoValidator _sut;
    public AlterarMetodoPagamentoDtoValidatorTests()
    {
        _usuarioRepository = Substitute.For<IUsuarioRepository>();
        _sut = new AlterarMetodoPagamentoDtoValidator(_usuarioRepository);
    }

    [Fact]
    public async Task TestValidate_DeveFalhar_QuandoMetodoPagamentoNaoForEncontrado()
    {
        // Arrange
        _usuarioRepository.ObterPorId(Arg.Any<string>(), Arg.Any<Expression<Func<Usuario, object>>>())
            .ReturnsNull();

        var alterarMetodoPagamentoDto = new AlterarMetodoPagamentoDto(
            _fixture.Create<string>(),
            _fixture.Create<string>(),
            _fixture.Create<BandeiraCartao>(),
            _fixture.Create<string>(),
            _fixture.Create<DateTime>(),
            _fixture.Create<TipoPagamento>());

        // Act
        var result = await _sut.TestValidateAsync(alterarMetodoPagamentoDto);

        // Assert
        result.ShouldHaveValidationErrorFor(p => p.MetodoPagamentoId);
    }
}
