using MataAtlantica.Domain.Abstract.Repositories;
using MataAtlantica.Domain.Entidades;
using MataAtlantica.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MataAtlantica.Infrastructure.Repositories;

public class FornecedorRepository : BaseRepository<Fornecedor>, IFornecedorRepository
{
    public FornecedorRepository(MataAtlanticaDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Fornecedor> ObterPorCpfCnpj(string cpfCnpj)
        => await _dbSet.FirstOrDefaultAsync(fornecedor => fornecedor.CpfCnpj == cpfCnpj);
}
