using FluentValidation;
using MataAtlantica.API.Domain.Abstract.Repositories;
using MataAtlantica.API.Domain.Erros;

namespace MataAtlantica.API.Application.Models;

public class AdicionarThumbnailValidator : AbstractValidator<AdicionarThumbnailProdutoDto>
{
    public static string[] FormatosPermitidos = { ".jpeg", ".png" };
    public const int TamanhoMaximoImagem = 1_000_000;

    public AdicionarThumbnailValidator(IProdutoRepository produtoRepository)
    {
        RuleFor(p => p.ProdutoId)
            .MustAsync(async (produtoId, cancellationToken) => await produtoRepository.ObterPorId(produtoId) != null)
            .WithErrorCode(nameof(BusinessErrors.ProdutoNaoEncontrado))
            .WithMessage(nameof(BusinessErrors.ProdutoNaoEncontrado));

        RuleFor(p => p.Thumbnail)
            .NotNull()
            .WithErrorCode(nameof(BusinessErrors.NenhumImagemPassada))
            .WithMessage(BusinessErrors.NenhumImagemPassada.Message)
            .Must(imagem => imagem != null && FormatosPermitidos.Any(formato => imagem.FileName.EndsWith(formato)))
            .WithErrorCode(nameof(BusinessErrors.ArquivoComFormatoInvalido))
            .WithMessage(BusinessErrors.ArquivoComFormatoInvalido.Message)
            .Must(imagem => imagem?.Length < TamanhoMaximoImagem)
            .WithErrorCode(nameof(BusinessErrors.ImagemMuitoGrande))
            .WithMessage(BusinessErrors.ImagemMuitoGrande.Message);
    }
}
