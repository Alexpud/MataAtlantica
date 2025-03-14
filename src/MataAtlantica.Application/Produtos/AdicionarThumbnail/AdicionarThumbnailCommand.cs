using FluentResults;
using FluentValidation.Results;
using MataAtlantica.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace MataAtlantica.Application.Produtos.AdicionarThumbnail;

public record AdicionarThumbnailCommand(string ProdutoId, IFormFile ArquivoImagem, int Ordem) : BaseCommand, IRequest<Result>
{
    public override ValidationResult Validate()
    {
        var validator = new AdicionarThumbnailCommandValidator();
        return validator.Validate(this);
    }
}
