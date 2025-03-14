using FluentResults;
using FluentValidation;
using MataAtlantica.Domain.Erros;
using MataAtlantica.Domain.Models.Produtos;
using MataAtlantica.Domain.Services;
using MediatR;

namespace MataAtlantica.Application.Produtos.AlterarProduto;


public class CommandHandler(ProdutoService produtoService) : IRequestHandler<AlterarProdutoCommand, Result<ProdutoDto>>
{
    private readonly ProdutoService _produtoService = produtoService;
    public async Task<Result<ProdutoDto>> Handle(AlterarProdutoCommand command, CancellationToken cancellationToken)
    {
        var dto = new AlterarProdutoDto(command.ProdutoId, command.Nome);
        return await _produtoService.Alterar(dto);
    }
}
