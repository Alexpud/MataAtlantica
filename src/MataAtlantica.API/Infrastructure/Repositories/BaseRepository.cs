using MataAtlantica.API.Domain.Entidades;
using MataAtlantica.API.Domain.Repositories.Abstract;
using MataAtlantica.API.Domain.Specifications;
using MataAtlantica.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MataAtlantica.API.Infrastructure.Repositories;

public abstract class BaseRepository<TEntity>(MataAtlanticaDbContext dbContext) : IBaseRepository<TEntity> where TEntity : EntidadeBase
{
    protected readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();
    private readonly DbContext _dbContext = dbContext;

    public void Adicionar(TEntity entidade)
    {
        _dbSet.Add(entidade);
    }

    public void Atualizar(TEntity entidade)
    {
        _dbSet.Update(entidade);
    }

    public IQueryable<TEntity> AsQueryable()
        => _dbSet.AsQueryable();

    public async Task Commit()
    {
        await _dbContext.SaveChangesAsync();
    }

    public IQueryable<TEntity> BuscarPorSpec(BaseSpecification<TEntity> specification)
    {
        var query = _dbSet.Where(specification.ToExpression());
        if (specification.IncludeExpression != null)
            query = specification.IncludeExpression(query);

        return query;
    }

    // Essa implementacao ficou redundante com specification
    public IQueryable<TEntity> FilterBy<TProperty>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProperty>> include)
    {
        var query = _dbSet.Where(predicate);
        if (include != null)
            query = query.Include(include);

        return query;
    }

    public async Task<TEntity> ObterPorId(string id)
    {
        return await _dbSet.FirstOrDefaultAsync(entity => entity.Id == id); 
    }

    // Essa implementacao ficou redundante com specification
    public async Task<TEntity> ObterPorId<TProperty>(string id, Expression<Func<TEntity, TProperty>> include)
    {
        if (include == null)
            return await ObterPorId(id);
        return await _dbSet.Include(include).FirstOrDefaultAsync(entity => entity.Id == id);
    }
}
