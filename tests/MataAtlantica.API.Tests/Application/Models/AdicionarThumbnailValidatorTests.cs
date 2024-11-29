using FluentResults;
using FluentValidation.TestHelper;
using MataAtlantica.API.Application.Models;
using MataAtlantica.API.Domain.Abstract.Repositories;
using MataAtlantica.API.Domain.Entidades;
using MataAtlantica.API.Domain.Erros;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using System.Text;

namespace MataAtlantica.API.Tests.Application.Services;
public class AdicionarThumbnailValidatorTests
{
    private readonly AdicionarImagemProdutoValidator _sut;
    private readonly IProdutoRepository _produtoRepository;
    public AdicionarThumbnailValidatorTests()
    {
        _produtoRepository = Substitute.For<IProdutoRepository>();
        _sut = new(_produtoRepository);
    }

    [Trait("Feature", "Adicionar imagem thumbnail a produto")]
    [Fact(DisplayName = "TestValidate deve ter erro de validacao para produtoId quando produto nao e encontrado")]
    public async Task TestValidate_DeveTerErroDeValidacaoParaProdutoId_QuandoProdutoNaoEEncontrado()
    {
        // Arrange
        var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
        IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.pdf");
        var model = new AdicionarImagemProdutoDto()
        {
            ArquivoImagem = file,
        };

        // Act
        var result = await _sut.TestValidateAsync(model);

        // Assert
        result.ShouldHaveValidationErrorFor(p => p.ArquivoImagem);
        var errors = result.Errors.Where(p => p.PropertyName == nameof(AdicionarImagemProdutoDto.ArquivoImagem));
        Assert.NotEmpty(result.Errors
            .Where(p => p.PropertyName == nameof(AdicionarImagemProdutoDto.ProdutoId)
                && p.ErrorCode == nameof(BusinessErrors.ProdutoNaoEncontrado)));
    }

    [Trait("Feature", "Adicionar imagem thumbnail a produto")]
    [Fact(DisplayName = "TestValidate nao deve ter erro de validacao para produtoId quando for valido")]
    public async Task TestValidate_NaoDeveTerErroDeValidacaoParaProdutoId_QuandoProdutoIdForValido()
    {
        // Arrange
        var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
        IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.pdf");
        var model = new AdicionarImagemProdutoDto()
        {
            ProdutoId = "PRODUTOiD",
            ArquivoImagem = file
        };

        _produtoRepository.ObterPorId(Arg.Any<string>())
            .Returns(new Produto());

        // Act
        var result = await _sut.TestValidateAsync(model);

        // Assert
        result.ShouldNotHaveValidationErrorFor(p => p.ProdutoId);
    }

    [Trait("Feature", "Adicionar imagem thumbnail a produto")]
    [Fact(DisplayName = "TestValidate deve ter erro de validacao para thumbnails quando nenhuma thumbnail for passada")]
    public async Task TestValidate_DeveTerErroDeValidacaoParaThumbnails_QuandoNenhumArquivoForPassado()
    {
        // Arrange
        var model = new AdicionarImagemProdutoDto();

        // Act
        var result = await _sut.TestValidateAsync(model);

        // Assert
        result.ShouldHaveValidationErrorFor(p => p.ArquivoImagem);
        var errors = result.Errors.Where(p => p.PropertyName == nameof(AdicionarImagemProdutoDto.ArquivoImagem));
        Assert.NotEmpty(result.Errors
            .Where(p => p.PropertyName == nameof(AdicionarImagemProdutoDto.ArquivoImagem)
                && p.ErrorCode == nameof(BusinessErrors.NenhumImagemPassada)));
    }

    [Trait("Feature", "Adicionar imagem thumbnail a produto")]
    [Fact(DisplayName = "TestValidate deve ter erro de validacao para thumbnails algum arquivo possuir extensao errada")]
    public async Task TestValidate_DeveTerErroDeValidacaoParaThumbnails_QuandoAlgumArquivoPossuirExtensaoInvalida()
    {
        // Arrange
        var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
        IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.pdf");
        var model = new AdicionarImagemProdutoDto
        {
            ArquivoImagem = file
        };

        // Act
        var result = await _sut.TestValidateAsync(model);

        // Assert
        result.ShouldHaveValidationErrorFor(p => p.ArquivoImagem);
        var errors = result.Errors.Where(p => p.PropertyName == nameof(AdicionarImagemProdutoDto.ArquivoImagem));
        Assert.NotEmpty(result.Errors
            .Where(p => p.PropertyName == nameof(AdicionarImagemProdutoDto.ArquivoImagem)
                && p.ErrorCode == nameof(BusinessErrors.ArquivoComFormatoInvalido)));
    }
}
