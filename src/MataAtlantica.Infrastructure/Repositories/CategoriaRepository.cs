using MataAtlantica.Domain.Abstract.Repositories;
using MataAtlantica.Domain.Entidades;
using MataAtlantica.Infrastructure.Data;

namespace MataAtlantica.Infrastructure.Repositories;

public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(MataAtlanticaDbContext dbContext) : base(dbContext)
    {
    }
}