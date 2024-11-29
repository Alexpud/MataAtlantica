using MataAtlantica.API.Domain.Models;
using MataAtlantica.API.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MataAtlantica.API.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CategoriasController(CategoriaService categoriaService) : ControllerBase
{
    private readonly CategoriaService _categoriaService = categoriaService;

    /// <summary>
    /// Adiciona uma nova categoria
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(CategoriaDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Criar(AdicionarCategoriaRequest model)
    {
        var dto = new AdicionarCategoriaDto(model.Nome);
        return Ok(await _categoriaService.Adicionar(dto));
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
        var dto = new AdicionarCategoriaDto(model.Nome);
        return Ok(await _categoriaService.AdicionarSubCategoria(id, model.Nome));
    }

    /// <summary>
    /// Lista as categorias com suas subCategorias
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<CategoriaDto>), (int)HttpStatusCode.OK)]
    public IActionResult Listar()
    {
        return Ok(_categoriaService.ListarCategoriasComoArvore());
    }
}

public record struct AdicionarCategoriaRequest(string Nome);
