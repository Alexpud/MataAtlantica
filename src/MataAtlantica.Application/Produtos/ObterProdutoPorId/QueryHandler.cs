using MataAtlantica.Domain.Models.Produtos;
using MataAtlantica.Domain.Services;
using MediatR;

namespace MataAtlantica.Application.Produtos.ObterProdutoPorId;

public record class ObterProdutoQuery(string ProdutoId) : IRequest<ProdutoDto> { }


public class QueryHandler(ProdutoService produtoService) : IRequestHandler<ObterProdutoQuery, ProdutoDto>
{
    private readonly ProdutoService _produtoService = produtoService;

    public async Task<ProdutoDto> Handle(ObterProdutoQuery request, CancellationToken cancellationToken)
    {
        return await _produtoService.ObterPorId(request.ProdutoId);
    }
}
