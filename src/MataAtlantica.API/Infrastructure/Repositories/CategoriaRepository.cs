using MataAtlantica.API.Infrastructure.Data;
using MataAtlantica.Domain.Abstract.Repositories;
using MataAtlantica.Domain.Entidades;

namespace MataAtlantica.API.Infrastructure.Repositories;

public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(MataAtlanticaDbContext dbContext) : base(dbContext)
    {
    }
}