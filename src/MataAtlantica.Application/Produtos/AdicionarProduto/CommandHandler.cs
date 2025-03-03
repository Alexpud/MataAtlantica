using FluentResults;
using MataAtlantica.Domain.Models.Produtos;
using MataAtlantica.Domain.Services;
using MediatR;

namespace MataAtlantica.Application.Produtos.AdicionarProduto;

public class CommandHandler(ProdutoService produtoService) : IRequestHandler<AdicionarProdutoCommand, Result<ProdutoDto>>
{
    private readonly ProdutoService _produtoService = produtoService;
    public async Task<Result<ProdutoDto>> Handle(AdicionarProdutoCommand request, CancellationToken cancellationToken)
    {
        var dto = new AdicionarProdutoDto(
            request.Nome,
            request.CategoriaId,
            request.Preco,
            request.Descricao,
            request.FornecedorId,
            request.Marca);

        return await _produtoService.Adicionar(dto);
    }
}
