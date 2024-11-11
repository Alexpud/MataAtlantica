using MataAtlantica.API.Domain.Entidades;

namespace MataAtlantica.API.Domain.Abstract.Repositories;

public interface IFornecedorRepository : IBaseRepository<Fornecedor>
{
    Task<Fornecedor> ObterPorCpfCnpj(string cpfCnpj);
}