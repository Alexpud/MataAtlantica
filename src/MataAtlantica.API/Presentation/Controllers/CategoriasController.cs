using MataAtlantica.API.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace MataAtlantica.API.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController(CategoriaService categoriaService) : ControllerBase
{
    private readonly CategoriaService _categoriaService = categoriaService;

    [HttpPost]
    public async Task<IActionResult> Criar(AdicionarCategoriaViewModel model)
    {
        var dto = new Domain.Models.AdicionarCategoriaArgs(model.Nome);
        return Ok(await _categoriaService.Adicionar(dto));
    }
}

public record struct AdicionarCategoriaViewModel(string Nome);
