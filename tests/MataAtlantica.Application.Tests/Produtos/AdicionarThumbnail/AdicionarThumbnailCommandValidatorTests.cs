using FluentValidation.TestHelper;
using MataAtlantica.Application.Produtos.AdicionarThumbnail;
using MataAtlantica.Domain.Abstract.Repositories;
using MataAtlantica.Domain.Erros;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace MataAtlantica.Application.Tests.Produtos.AdicionarThumbnail;
public class AdicionarThumbnailCommandValidatorTests
{
    private readonly AdicionarThumbnailCommandValidator _sut;
    private readonly IProdutoRepository _produtoRepository;
    public AdicionarThumbnailCommandValidatorTests()
    {
        _sut = new();
    }

    [Trait("Feature", "Adicionar imagem thumbnail a produto")]
    [Fact(DisplayName = "TestValidate deve ter erro de validacao para thumbnails quando nenhuma thumbnail for passada")]
    public async Task TestValidate_DeveTerErroDeValidacaoParaThumbnails_QuandoNenhumArquivoForPassado()
    {
        // Arrange
        var model = new AdicionarThumbnailCommand();

        // Act
        var result = await _sut.TestValidateAsync(model);

        // Assert
        result.ShouldHaveValidationErrorFor(p => p.ArquivoImagem);
        var errors = result.Errors.Where(p => p.PropertyName == nameof(AdicionarThumbnailCommand.ArquivoImagem));
        Assert.NotEmpty(result.Errors
            .Where(p => p.PropertyName == nameof(AdicionarThumbnailCommand.ArquivoImagem)
                && p.ErrorCode == nameof(BusinessErrors.NenhumImagemPassada)));
    }

    [Trait("Feature", "Adicionar imagem thumbnail a produto")]
    [Fact(DisplayName = "TestValidate deve ter erro de validacao para thumbnails algum arquivo possuir extensao errada")]
    public async Task TestValidate_DeveTerErroDeValidacaoParaThumbnails_QuandoAlgumArquivoPossuirExtensaoInvalida()
    {
        // Arrange
        var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
        IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.pdf");
        var model = new AdicionarThumbnailCommand
        {
            ArquivoImagem = file
        };

        // Act
        var result = await _sut.TestValidateAsync(model);

        // Assert
        result.ShouldHaveValidationErrorFor(p => p.ArquivoImagem);
        var errors = result.Errors.Where(p => p.PropertyName == nameof(AdicionarThumbnailCommand.ArquivoImagem));
        Assert.NotEmpty(result.Errors
            .Where(p => p.PropertyName == nameof(AdicionarThumbnailCommand.ArquivoImagem)
                && p.ErrorCode == nameof(BusinessErrors.ArquivoComFormatoInvalido)));
    }
}
