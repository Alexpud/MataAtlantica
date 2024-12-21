using MataAtlantica.Domain.Entidades;
using MataAtlantica.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

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

public abstract class BaseSpecification<T> where T : EntidadeBase
{
    protected Expression<Func<T, bool>> Predicate { get; set; }

    public Func<IQueryable<T>, IIncludableQueryable<T, object>> IncludeExpression { get; set; }

    public Expression<Func<T, bool>> ToExpression() => Predicate;
}