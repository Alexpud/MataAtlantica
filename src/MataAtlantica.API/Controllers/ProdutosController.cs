using MataAtlantica.API.Models;
using MataAtlantica.Domain.Models;
using MataAtlantica.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MataAtlantica.API.Controllers;

[Route("api/[controller]")]
public class ProdutosController(ProdutoService service) : BaseController
{
    private readonly ProdutoService _service = service;

    /// <summary>
    /// Obtem produto pelo Id
    /// </summary>
    /// <param name="id">ID do produto</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProdutoDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> ObterPorId(string id)
    {
        var produto = await _service.ObterPorId(id);
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
        var dto = new AdicionarProdutoDto(
            model.Nome,
            model.CategoriaId,
            model.Preco,
            model.Descricao,
            model.FornecedorId,
            model.Marca);
        var result = await _service.Adicionar(dto);
        return result.IsFailed ? HandleFailedResult(result) : Ok(result.Value);
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
        var dto = new AlterarProdutoDto(
            id,
            model.Nome);
        var result = await _service.Alterar(dto);
        return result.IsFailed ? HandleFailedResult(result) : Ok(result.Value);
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
        var dto = new BuscarProdutosArgs()
        {
            Categoria = model.Categoria,
            Fornecedor = model.Fornecedor,
            Nome = model.Nome,
        };
        return Ok(_service.BuscarProdutos(dto));
    }
}