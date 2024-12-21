using MataAtlantica.Application.Categorias.AdicionarCategoria;
using MataAtlantica.Application.Categorias.AdicionarSubCategoria;
using MataAtlantica.Application.Categorias.ListarCategorias;
using MataAtlantica.Domain.Models;
using MataAtlantica.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MataAtlantica.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CategoriasController(IMediator mediator, CategoriaService categoriaService) : BaseController
{
    private readonly CategoriaService _categoriaService = categoriaService;
    private readonly IMediator _mediator = mediator;

    /// <summary>
    /// Adiciona uma nova categoria
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(CategoriaDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Adicionar(AdicionarCategoriaRequest model)
    {
        var command = new AdicionarCategoriaCommand(model.Nome);
        return Ok(await _mediator.Send(command));
    }

    /// <summary>
    /// Adiciona uma subcategoria a uma categoria existente
    /// </summary>
    /// <param name="id">Id da categoria</param>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("{id}")]
    [ProducesResponseType(typeof(CategoriaDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> AdicionarSubCategoria(string id, AdicionarCategoriaRequest model)
    {
        var command = new AdicionarSubCategoriaCommand(id, model.Nome);
        var result = await _mediator.Send(command);
        return result.IsFailed ? HandleFailedResult(result) : Ok(result);
    }

    /// <summary>
    /// Lista as categorias com suas subCategorias
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<CategoriaDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Listar()
    {
        return Ok(await _mediator.Send(new ListarCategoriasQuery()));
    }
}

public record struct AdicionarCategoriaRequest(string Nome);
