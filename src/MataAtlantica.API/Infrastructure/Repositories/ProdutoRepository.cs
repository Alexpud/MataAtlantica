using MataAtlantica.API.Domain.Entidades;
using MataAtlantica.API.Domain.Repositories.Abstract;
using MataAtlantica.API.Infrastructure.Data;

namespace MataAtlantica.API.Infrastructure.Repositories;

public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
{
    public ProdutoRepository(MataAtlanticaDbContext dbContext) : base(dbContext)
    {
        
    }
}
