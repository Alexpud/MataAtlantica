using MataAtlantica.Domain.Models.Produtos;
using MataAtlantica.Domain.Services;
using MediatR;

namespace MataAtlantica.Application.Produtos.BuscarProdutos;

public record BuscarProdutosQuery(string Nome, string Categoria, string Fornecedor) : IRequest<IQueryable<ProdutoDto>> { }

public class QueryHandler(ProdutoService produtoService) : IRequestHandler<BuscarProdutosQuery, IQueryable<ProdutoDto>>
{
    private readonly ProdutoService _produtoService = produtoService;
    public Task<IQueryable<ProdutoDto>> Handle(BuscarProdutosQuery query, CancellationToken cancellationToken)
    {
        var args = new BuscarProdutosArgs()
        {
            Nome = query.Nome,
            Categoria = query.Categoria,
            Fornecedor = query.Fornecedor
        };
        return Task.FromResult(_produtoService.BuscarProdutos(args));
    }
}
