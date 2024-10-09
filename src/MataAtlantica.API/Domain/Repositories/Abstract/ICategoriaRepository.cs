using MataAtlantica.API.Domain.Entidades;
using System.Linq.Expressions;

namespace MataAtlantica.API.Domain.Repositories.Abstract;

public interface ICategoriaRepository : IBaseRepository<Categoria>
{
}

public interface IBaseRepository<TEntity> where TEntity : EntidadeBase
{
    IQueryable<TEntity> FilterBy<TProperty>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProperty>> include);
    IQueryable<TEntity> AsQueryable();
    void Adicionar(TEntity categoria);
    Task Commit();
}
