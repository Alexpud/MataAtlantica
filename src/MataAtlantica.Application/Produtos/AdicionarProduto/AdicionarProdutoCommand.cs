using FluentResults;
using FluentValidation.Results;
using MataAtlantica.Application.Common;
using MataAtlantica.Domain.Models.Produtos;
using MediatR;

namespace MataAtlantica.Application.Produtos.AdicionarProduto;

public record AdicionarProdutoCommand(
    string Nome,
    string CategoriaId,
    float Preco,
    string Descricao,
    string FornecedorId,
    string Marca
    ) : BaseCommand, IRequest<Result<ProdutoDto>>
{
    public override ValidationResult Validate()
    {
        var validator = new AdicionarProdutoCommandValidator();
        return validator.Validate(this);
    }
}
