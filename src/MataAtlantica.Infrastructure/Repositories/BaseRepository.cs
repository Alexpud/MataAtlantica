using MataAtlantica.Domain.Abstract.Repositories;
using MataAtlantica.Domain.Entidades;
using MataAtlantica.Domain.Specifications;
using MataAtlantica.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MataAtlantica.Infrastructure.Repositories;

public abstract class BaseRepository<TEntity>(MataAtlanticaDbContext dbContext) : IBaseRepository<TEntity> where TEntity : EntidadeBase, new()
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

    public void Excluir(string id)
    {
        _dbSet.Remove(new TEntity() { Id = id });
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

    public async Task<TEntity> ObterPorSpec(BaseSpecification<TEntity> specification)
    {
        return await _dbSet.FirstOrDefaultAsync(specification.ToExpression());
    }
}
