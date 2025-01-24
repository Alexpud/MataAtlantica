using MataAtlantica.Domain.Entidades;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace MataAtlantica.Domain.Specifications;

public abstract class BaseSpecification<T> where T : EntidadeBase
{
    protected Expression<Func<T, bool>> Predicate { get; set; }

    public Func<IQueryable<T>, IIncludableQueryable<T, object>> IncludeExpression { get; set; }

    public Expression<Func<T, bool>> ToExpression() => Predicate;
}