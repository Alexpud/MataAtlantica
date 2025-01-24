using MataAtlantica.API.Models;
using MataAtlantica.API.Models.Produtos;
using MataAtlantica.Application.Produtos.AdicionarProduto;
using MataAtlantica.Application.Produtos.AlterarProduto;
using MataAtlantica.Application.Produtos.BuscarProdutos;
using MataAtlantica.Application.Produtos.ObterProdutoPorId;
using MataAtlantica.Domain.Models.Produtos;
using MataAtlantica.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MataAtlantica.API.Controllers;

[Route("api/[controller]")]
public class ProdutosController(IMediator mediator, ProdutoService service) : BaseController
{
    private readonly ProdutoService _service = service;
    private readonly IMediator _mediator = mediator;

    /// <summary>
    /// Obtem produto pelo Id
    /// </summary>
    /// <param name="id">ID do produto</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProdutoDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> ObterPorId(string id)
    {
        var produto = await _mediator.Send(new ObterProdutoQuery(id));
        return produto == null ? NoContent() : Ok(produto);
    }

    /// <summary>
    /// Cria um novo produto
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <remarks>
    /// Exemplo de requisicao
    /// 
    ///     POST /Produtos
    ///     {
    ///         "Nome": "Nome de produto",
    ///         "Descricao": "Produto muito interessante",
    ///         "CategoriaId": "XXXX-XXX-XXXX",
    ///         "Preco": "5.00",
    ///         "FornecedorId": "XXXXXXX-XXXXXXX",
    ///         "Marca": "marca legal"
    ///     }
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(typeof(ProdutoDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BadRequestResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> AdicionarProduto(AdicionarProdutoRequest model)
    {
        var command = new AdicionarProdutoCommand(
            model.Nome,
            model.CategoriaId,
            model.Preco,
            model.Descricao,
            model.FornecedorId,
            model.Marca);
        return HandleResult(await _mediator.Send(command));
    }

    /// <summary>
    /// Altera um produto existente
    /// </summary>
    /// <param name="id">ID do produto existente</param>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ProdutoDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BadRequestResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> AlterarProduto(string id, AlterarProdutoRequest model)
    {
        var command = new AlterarProdutoCommand(
            id,
            model.Nome);
        return HandleResult(await _mediator.Send(command));
    }

    /// <summary>
    /// Busca os produtos filtrados pelos parametros passados
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(ProdutoDto), (int)HttpStatusCode.OK)]
    public IActionResult BuscarProdutos([FromQuery] BuscarProdutosRequest model)
    {
        var query = new BuscarProdutosQuery(model.Nome, model.Categoria, model.Fornecedor);
        return Ok(_mediator.Send(query));
    }
}