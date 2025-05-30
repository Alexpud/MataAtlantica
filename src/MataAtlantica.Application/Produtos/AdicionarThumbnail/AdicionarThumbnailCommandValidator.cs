﻿using FluentValidation;
using MataAtlantica.Domain.Erros;

namespace MataAtlantica.Application.Produtos.AdicionarThumbnail;

public class AdicionarThumbnailCommandValidator : AbstractValidator<AdicionarThumbnailCommand>
{
    public static string[] FormatosPermitidos = { ".jpeg", ".png" };
    public const int TamanhoMaximoImagem = 1_000_000;
    public AdicionarThumbnailCommandValidator()
    {
        RuleFor(p => p.ArquivoImagem)
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
