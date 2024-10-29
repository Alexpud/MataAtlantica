using MataAtlantica.API.Presentation.Models;
using Microsoft.AspNetCore.Mvc;

namespace MataAtlantica.API.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    [HttpGet("{id}")]
    public IActionResult ObterPorId(string id)
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult CriarProduto(CriarProduto model)
    {
        return Ok();
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