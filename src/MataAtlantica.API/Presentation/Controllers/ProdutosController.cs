using MataAtlantica.API.Domain.Models;
using MataAtlantica.API.Domain.Models.Validators;
using MataAtlantica.API.Domain.Services;
using MataAtlantica.API.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MataAtlantica.API.Presentation.Controllers;

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
    public async Task<IActionResult> CriarProduto(Models.CriarProduto model)
    {
        var dto = new Domain.Models.CriarProduto(
            model.Nome,
            model.CategoriaId,
            model.Preco,
            model.Descricao,
            model.FornecedorId,
            model.Marca);
        var result = await _service.CriarProduto(dto);
        return result.IsFailed ? HandleFailedResult(result) : Ok(result.Value);
    }

    /// <summary>
    /// Busca os produtos filtrados pelos parametros passados
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult BuscarProdutos(BuscarProdutosArgs model)
    {
        return Ok();
    }
}