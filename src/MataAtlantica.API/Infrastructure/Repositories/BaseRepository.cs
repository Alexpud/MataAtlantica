using MataAtlantica.API.Domain.Entidades;
using MataAtlantica.API.Domain.Repositories.Abstract;
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

    public IQueryable<TEntity> AsQueryable()
        => _dbSet.AsQueryable();

    public async Task Commit()
    {
        await _dbContext.SaveChangesAsync();
    }

    public IQueryable<TEntity> FilterBy<TProperty>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProperty>> include)
    {
        var query = _dbSet.Where(predicate);
        if (include != null)
            query = query.Include(include);
        return query;
    }
}
