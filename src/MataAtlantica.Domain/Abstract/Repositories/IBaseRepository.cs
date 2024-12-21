using MataAtlantica.Domain.Entidades;
using MataAtlantica.Domain.Specifications;
using System.Linq.Expressions;

namespace MataAtlantica.Domain.Abstract.Repositories;

public interface IBaseRepository<TEntity> where TEntity : EntidadeBase
{
    Task<TEntity> ObterPorId(string id);
    Task<TEntity> ObterPorId<TProperty>(string id, Expression<Func<TEntity, TProperty>> include);
    IQueryable<TEntity> AsQueryable();
    void Adicionar(TEntity categoria);
    Task Commit();
    void Atualizar(TEntity entidade);
    IQueryable<TEntity> BuscarPorSpec(BaseSpecification<TEntity> specification);
}
