using FluentValidation.TestHelper;
using MataAtlantica.Domain.Abstract.Repositories;
using MataAtlantica.Domain.Entidades;
using MataAtlantica.Domain.Erros;
using MataAtlantica.Domain.Models;
using MataAtlantica.Domain.Models.Validators;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace MataAtlantica.Domain.Tests.Models;
public class CriarProdutoValidatorTests
{
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly IFornecedorRepository _fornecedorRepository;
    private readonly CriarProdutoValidator _sut;

    public CriarProdutoValidatorTests()
    {
        _categoriaRepository = Substitute.For<ICategoriaRepository>();
        _fornecedorRepository = Substitute.For<IFornecedorRepository>();
        _sut = new CriarProdutoValidator(_categoriaRepository, _fornecedorRepository);
    }

    [Trait("Feature", "Criar Produto")]
    [Fact(DisplayName = "TestValidate deve falhar com erro em categoriaId quando categoria nao foi encontrada")]
    public async Task TestValidate_DeveFalharComErroEmCategoriaId_QuandoCategoriaNaoFoiEncontrada()
    {
        // Arrange
        var criarProduto = new AdicionarProdutoDto(
            Nome: string.Empty,
            CategoriaId: string.Empty,
            Preco: 0,
            Descricao: string.Empty,
            FornecedorId: string.Empty,
            Marca: string.Empty);

        _categoriaRepository.ObterPorId(Arg.Any<string>())
            .ReturnsNull();

        // Act
        var validationResult = await _sut.TestValidateAsync(criarProduto);

        // Assert
        var error = validationResult
            .Errors
            .FirstOrDefault(p => p.PropertyName == nameof(criarProduto.CategoriaId) && p.ErrorCode == nameof(BusinessErrors.CategoriaNaoEncontrada));
        Assert.Multiple(
            () => validationResult.ShouldHaveValidationErrorFor(p => p.CategoriaId),
            () => Assert.NotNull(error),
            () => Assert.Equal(BusinessErrors.CategoriaNaoEncontrada.Message, error?.ErrorMessage));
    }

    [Trait("Feature", "Criar Produto")]
    [Fact(DisplayName = "TestValidate nao deve falhar com erro em categoriaId quando categoria for valida")]
    public async Task TestValidate_NaoDeveTerErroEmCategoriaId_QuandoCategoriaForValida()
    {
        // Arrange
        var criarProduto = new AdicionarProdutoDto(
            Nome: string.Empty,
            CategoriaId: "alguma categoria",
            Preco: 0,
            Descricao: string.Empty,
            FornecedorId: string.Empty,
            Marca: string.Empty);

        _categoriaRepository.ObterPorId(Arg.Any<string>())
            .Returns(new Categoria());

        // Act
        var validationResult = await _sut.TestValidateAsync(criarProduto);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(p => p.CategoriaId);
    }

    [Trait("Feature", "Criar Produto")]
    [Fact(DisplayName = "TestValidate deve falhar com erro em fornecedorId quando fornecedor nao for encontrado")]
    public async Task TestValidate_DeveFalharComErroEmFornecedorId_QuandoFornecedorNaoForEncontrado()
    {
        // Arrange
        var criarProduto = new AdicionarProdutoDto(
            Nome: string.Empty,
            CategoriaId: string.Empty,
            Preco: 0,
            Descricao: string.Empty,
            FornecedorId: string.Empty,
            Marca: string.Empty);

        _fornecedorRepository.ObterPorId(Arg.Any<string>())
            .ReturnsNull();

        // Act
        var validationResult = await _sut.TestValidateAsync(criarProduto);

        // Assert
        var error = validationResult
            .Errors
            .FirstOrDefault(p => p.PropertyName == nameof(criarProduto.FornecedorId) && p.ErrorCode == nameof(BusinessErrors.FornecedorNaoEncontrado));
        Assert.Multiple(
            () => validationResult.ShouldHaveValidationErrorFor(p => p.FornecedorId),
            () => Assert.NotNull(error),
            () => Assert.Equal(BusinessErrors.FornecedorNaoEncontrado.Message, error?.ErrorMessage));
    }

    [Trait("Feature", "Criar Produto")]
    [Fact(DisplayName = "TestValidate nao deve falhar com erro em fornecedorId quando fornecedor for valido")]
    public async Task TestValidate_NaoDeveTerErroEmFornecedorId_QuandoFornecedorForValido()
    {
        // Arrange
        var criarProduto = new AdicionarProdutoDto(
            Nome: string.Empty,
            CategoriaId: string.Empty,
            Preco: 0,
            Descricao: string.Empty,
            FornecedorId: "algum fornecedor",
            Marca: string.Empty);

        _fornecedorRepository.ObterPorId(Arg.Any<string>())
            .Returns(new Fornecedor());

        // Act
        var validationResult = await _sut.TestValidateAsync(criarProduto);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(p => p.FornecedorId);
    }
}
