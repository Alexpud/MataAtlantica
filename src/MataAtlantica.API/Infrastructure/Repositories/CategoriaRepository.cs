using MataAtlantica.API.Domain.Abstract.Repositories;
using MataAtlantica.API.Domain.Entidades;
using MataAtlantica.API.Infrastructure.Data;

namespace MataAtlantica.API.Infrastructure.Repositories;

public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(MataAtlanticaDbContext dbContext) : base(dbContext)
    {
    }
}