using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using MataAtlantica.Application.Common;
using MataAtlantica.Domain.Erros;
using MataAtlantica.Domain.Models.Produtos;
using MediatR;

namespace MataAtlantica.Application.Produtos.AlterarProduto;

public record AlterarProdutoCommand(string ProdutoId, string Nome) : BaseCommand, IRequest<Result<ProdutoDto>> 
{
    public override ValidationResult Validate()
    {
        var validator = new AlterarProdutoCommandValidator();
        return validator.Validate(this);
    }
}


public class AlterarProdutoCommandValidator : AbstractValidator<AlterarProdutoCommand>
{
    public AlterarProdutoCommandValidator()
    {

        RuleFor(model => model.ProdutoId)
            .NotEmpty()
            .WithErrorCode(nameof(BusinessErrors.ProdutoNaoEncontrado))
            .WithMessage(BusinessErrors.ProdutoNaoEncontrado.Message);

        RuleFor(model => model.Nome)
            .NotEmpty()
            .WithErrorCode(nameof(EntityValidationErrors.NomeObrigatorioParaProduto))
            .WithMessage(EntityValidationErrors.NomeObrigatorioParaProduto.Message);
    }
}