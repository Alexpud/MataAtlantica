using FluentValidation;
using MataAtlantica.Domain.Erros;

namespace MataAtlantica.Application.Produtos.AdicionarProduto;

public class AdicionarProdutoCommandValidator : AbstractValidator<AdicionarProdutoCommand>
{
    public AdicionarProdutoCommandValidator()
    {
        RuleFor(produto => produto.Nome)
            .NotEmpty()
            .WithErrorCode(nameof(EntityValidationErrors.NomeObrigatorioParaProduto))
            .WithMessage(EntityValidationErrors.NomeObrigatorioParaProduto.Message);

        RuleFor(produto => produto.Descricao)
            .NotEmpty()
            .WithErrorCode(nameof(EntityValidationErrors.DescricaoObrigatorioParaProduto))
            .WithMessage(EntityValidationErrors.DescricaoObrigatorioParaProduto.Message);

        RuleFor(produto => produto.Marca)
            .NotEmpty()
            .WithErrorCode(nameof(EntityValidationErrors.MarcaObrigatoriaParaProduto))
            .WithMessage(EntityValidationErrors.MarcaObrigatoriaParaProduto.Message);

        RuleFor(produto => produto.Preco)
            .GreaterThan(0)
            .WithErrorCode(nameof(EntityValidationErrors.PrecoObrigatorioParaProduto))
            .WithMessage(EntityValidationErrors.PrecoObrigatorioParaProduto.Message);
    }
}
