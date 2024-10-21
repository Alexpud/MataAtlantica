using MataAtlantica.API.Domain.Entidades;
using System.Linq.Expressions;

namespace MataAtlantica.API.Domain.Specifications;

public class CategoriasRaizSpecification
{
    public Expression<Func<Categoria, bool>> Expression { get; private set; }
    public CategoriasRaizSpecification()
    {
        Expression = p => p.CategoriaPaiId == null;
    }
}
