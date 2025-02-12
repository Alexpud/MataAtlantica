using MataAtlantica.API.Models.Categorias;
using MataAtlantica.Application.Categorias.AdicionarCategoria;
using MataAtlantica.Application.Categorias.AdicionarSubCategoria;
using MataAtlantica.Application.Categorias.ListarCategorias;
using MataAtlantica.Domain.Models.Categorias;
using MataAtlantica.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using System.Net;
using System.Security.Claims;

namespace MataAtlantica.API.Controllers;

[Authorize]
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
        return HandleResult(await _mediator.Send(command));
    }

    /// <summary>
    /// Lista as categorias com suas subCategorias
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [OutputCache(Duration = 60, PolicyName = "CustomPolicy")]
    [ProducesResponseType(typeof(List<CategoriaDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Listar()
    {
        return Ok(await _mediator.Send(new ListarCategoriasQuery()));
    }
}