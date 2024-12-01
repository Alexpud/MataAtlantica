using MataAtlantica.API.Infrastructure.Data;
using MataAtlantica.Domain.Abstract.Repositories;
using MataAtlantica.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace MataAtlantica.API.Infrastructure.Repositories;

public class FornecedorRepository : BaseRepository<Fornecedor>, IFornecedorRepository
{
    public FornecedorRepository(MataAtlanticaDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Fornecedor> ObterPorCpfCnpj(string cpfCnpj) 
        => await _dbSet.FirstOrDefaultAsync(fornecedor => fornecedor.CpfCnpj == cpfCnpj);
}
