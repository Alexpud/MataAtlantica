using MataAtlantica.API.Domain.Entidades;
using MataAtlantica.API.Domain.Models;

namespace MataAtlantica.API.Domain.Repositories.Abstract;

public interface IProdutoRepository : IBaseRepository<Produto>
{
    IQueryable<ProdutoDto> Buscar(BuscarProdutosArgs args);
}
