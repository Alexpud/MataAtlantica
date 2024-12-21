using MataAtlantica.Domain.Entidades;

namespace MataAtlantica.Domain.Abstract.Repositories;

public interface IFornecedorRepository : IBaseRepository<Fornecedor>
{
    Task<Fornecedor> ObterPorCpfCnpj(string cpfCnpj);
}