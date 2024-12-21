using FluentValidation.TestHelper;
using MataAtlantica.Domain.Abstract.Repositories;
using MataAtlantica.Domain.Entidades;
using MataAtlantica.Domain.Erros;
using MataAtlantica.Domain.Models;
using MataAtlantica.Domain.Models.Validators;
using NSubstitute;

namespace MataAtlantica.Domain.Tests.Models;

public class AdicionarProdutoImagemValidatorTests
{
    private readonly AdicionarImagemProdutoValidator _sut;
    private readonly IProdutoRepository _produtoRepository;

    public AdicionarProdutoImagemValidatorTests()
    {
        _produtoRepository = Substitute.For<IProdutoRepository>();
        _sut = new(_produtoRepository);
    }

    [Trait("Feature", "Adicionar imagem thumbnail a produto")]
    [Fact(DisplayName = "TestValidate deve ter erro de validacao para produtoId quando produto nao e encontrado")]
    public async Task TestValidate_DeveTerErroDeValidacaoParaProdutoId_QuandoProdutoNaoEEncontrado()
    {
        // Arrange
        var model = new AdicionarImagemProdutoDto("asdas", 1);

        // Act
        var result = await _sut.TestValidateAsync(model);

        // Assert
        Assert
            .NotEmpty(result.Errors
                .Where(p => p.PropertyName == nameof(AdicionarImagemProdutoDto.ProdutoId)
                        && p.ErrorCode == nameof(BusinessErrors.ProdutoNaoEncontrado)));
    }

    [Trait("Feature", "Adicionar imagem thumbnail a produto")]
    [Fact(DisplayName = "TestValidate nao deve ter erro de validacao para produtoId quando for valido")]
    public async Task TestValidate_NaoDeveTerErroDeValidacaoParaProdutoId_QuandoProdutoIdForValido()
    {
        // Arrange
        var model = new AdicionarImagemProdutoDto("asdas", 1);

        _produtoRepository.ObterPorId(Arg.Any<string>())
            .Returns(new Produto());

        // Act
        var result = await _sut.TestValidateAsync(model);

        // Assert
        result.ShouldNotHaveValidationErrorFor(p => p.ProdutoId);
    }
}
