using MataAtlantica.Domain.Abstract.Repositories;
using MataAtlantica.Domain.Entidades;
using MataAtlantica.Infrastructure.Data;

namespace MataAtlantica.Infrastructure.Repositories;

public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(MataAtlanticaDbContext dbContext) : base(dbContext)
    {
    }
}