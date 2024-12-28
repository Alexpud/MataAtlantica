using FluentValidation;
using MataAtlantica.Domain.Abstract.Repositories;
using MataAtlantica.Domain.Erros;

namespace MataAtlantica.Domain.Models.Produtos;

public class AlterarProdutoDto
{
    public string ProdutoId { get; set; }
    public string Nome { get; set; }
    public AlterarProdutoDto(string produtoId, string nome)
    {
        ProdutoId = produtoId;
        Nome = nome;
    }
}

public class AlterarProdutoValidator : AbstractValidator<AlterarProdutoDto>
{
    public AlterarProdutoValidator(IProdutoRepository produtoRepository)
    {

        RuleFor(model => model.ProdutoId)
            .MustAsync(async (produtoId, cancellationToken) => await produtoRepository.ObterPorId(produtoId) != null)
            .WithErrorCode(nameof(BusinessErrors.ProdutoNaoEncontrado))
            .WithMessage(BusinessErrors.ProdutoNaoEncontrado.Message);

        RuleFor(model => model.Nome)
            .NotEmpty()
            .WithErrorCode(nameof(EntityValidationErrors.NomeObrigatorioParaProduto))
            .WithMessage(EntityValidationErrors.NomeObrigatorioParaProduto.Message);
    }

}