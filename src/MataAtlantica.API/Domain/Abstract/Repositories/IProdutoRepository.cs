using MataAtlantica.API.Domain.Entidades;
using MataAtlantica.API.Domain.Models;

namespace MataAtlantica.API.Domain.Abstract.Repositories;

public interface IProdutoRepository : IBaseRepository<Produto>
{
    IQueryable<ProdutoDto> Buscar(BuscarProdutosArgs args);
}
