using MataAtlantica.API.Domain.Entidades;
using MataAtlantica.API.Domain.Repositories.Abstract;
using MataAtlantica.API.Infrastructure.Data;

namespace MataAtlantica.API.Infrastructure.Repositories;

public class FornecedorRepository : BaseRepository<Fornecedor>, IFornecedorRepository
{
    public FornecedorRepository(MataAtlanticaDbContext dbContext) : base(dbContext)
    {
    }
}
