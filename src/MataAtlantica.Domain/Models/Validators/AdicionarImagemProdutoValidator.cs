using FluentValidation;
using MataAtlantica.Domain.Abstract.Repositories;
using MataAtlantica.Domain.Erros;

namespace MataAtlantica.Domain.Models.Validators;

public class AdicionarImagemProdutoValidator : AbstractValidator<AdicionarImagemProdutoDto>
{
    public AdicionarImagemProdutoValidator(IProdutoRepository produtoRepository)
    {
        RuleFor(p => p.ProdutoId)
            .MustAsync(async (produtoId, cancellationToken) => await produtoRepository.ObterPorId(produtoId) != null)
            .WithErrorCode(nameof(BusinessErrors.ProdutoNaoEncontrado))
            .WithMessage(nameof(BusinessErrors.ProdutoNaoEncontrado));
    }
}