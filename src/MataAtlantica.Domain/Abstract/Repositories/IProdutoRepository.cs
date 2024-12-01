using MataAtlantica.Domain.Entidades;
using MataAtlantica.Domain.Models;

namespace MataAtlantica.Domain.Abstract.Repositories;

public interface IProdutoRepository : IBaseRepository<Produto>
{
    IQueryable<ProdutoDto> Buscar(BuscarProdutosArgs args);
}
