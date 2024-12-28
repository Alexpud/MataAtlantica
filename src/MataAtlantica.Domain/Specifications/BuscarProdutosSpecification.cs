using MataAtlantica.Domain.Entidades;
using MataAtlantica.Domain.Models.Produtos;
using Microsoft.EntityFrameworkCore;

namespace MataAtlantica.Domain.Specifications;

// Based on steve ardallis specification pattern https://www.youtube.com/watch?v=i5FvDLsSrn0
public class BuscarProdutosSpecification : BaseSpecification<Produto>
{
    public BuscarProdutosSpecification(BuscarProdutosArgs args)
    {
        Predicate = (produto) =>
            (string.IsNullOrEmpty(args.Fornecedor) || produto.Fornecedor.Nome.Contains(args.Fornecedor))
            && (string.IsNullOrEmpty(args.Categoria) || produto.Categoria.Nome.Contains(args.Categoria))
            && (string.IsNullOrEmpty(args.Nome) || produto.Nome.Contains(args.Nome));

        IncludeExpression = (produto) => produto.Include(p => p.Fornecedor).Include(p => p.Categoria);
    }
}
