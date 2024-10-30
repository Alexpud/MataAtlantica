using MataAtlantica.API.Domain.Services;
using MataAtlantica.API.Presentation.Models;
using Microsoft.AspNetCore.Mvc;

namespace MataAtlantica.API.Presentation.Controllers;

[Route("api/[controller]")]
public class FornecedoresController(FornecedorService service) : BaseController
{
    private readonly FornecedorService _service = service;
    
    /// <summary>
    /// Obtem fornecedor pelo Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(string id)
    {
        var fornecedor = await _service.ObterPorId(id);
        return fornecedor == null ? NoContent() : Ok(fornecedor);
    }

    /// <summary>
    /// Cria um fornecedor
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Criar(CriarFornecedor model)
    {
        return Ok();
    }

    [HttpPut]
    public IActionResult Alterar(AlterarFornecedor model)
    {
        return Ok();
    }

    [HttpGet]
    public IActionResult Listar()
    {
        return Ok();
    }
}

