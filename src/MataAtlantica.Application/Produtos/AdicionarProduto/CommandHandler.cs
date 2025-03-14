using MataAtlantica.Application.Common;
using MataAtlantica.Domain.Models.Produtos;
using MataAtlantica.Domain.Services;
using MediatR;

namespace MataAtlantica.Application.Produtos.AdicionarProduto;

public class CommandHandler(ProdutoService produtoService) : IRequestHandler<AdicionarProdutoCommand, CommandResponse<ProdutoDto>>
{
    private readonly ProdutoService _produtoService = produtoService;
    public async Task<CommandResponse<ProdutoDto>> Handle(AdicionarProdutoCommand request, CancellationToken cancellationToken)
    {
        var dto = new AdicionarProdutoDto(
            request.Nome,
            request.CategoriaId,
            request.Preco,
            request.Descricao,
            request.FornecedorId,
            request.Marca);
        var response = new CommandResponse<ProdutoDto>();
        var result = await _produtoService.Adicionar(dto);
        if (result.IsSuccess)
            response.SetValue(result.Value);
        else
            response.WithErrors(result.Errors);
        return response;
    }
}
