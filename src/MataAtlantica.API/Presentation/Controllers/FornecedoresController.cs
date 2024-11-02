using MataAtlantica.API.Domain.Models;
using MataAtlantica.API.Domain.Services;
using MataAtlantica.API.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
    /// <remarks>
    /// Exemplo de requisicao:
    /// 
    ///     POST /Fornecedores
    ///     {
    ///         "Nome": "Nivea",
    ///         "Descricao": "Grande vendedora de cosméticos",
    ///         "CpfCnpj": "XXX.XXX.XXX/0001-XX",
    ///         "Telefone": "(17) 99999-9999",
    ///         "Localizacao": {
    ///             "Rua": "Rua das marias",
    ///             "Bairro": "Bairro das pedras",
    ///             "Numero": "1231",
    ///             "UF": "SP",
    ///             "CEP": "400000000",
    ///             "Cidade": "Florianopolis"
    ///         }
    ///      }
    /// 
    /// </remarks>
    /// <response code="201">Returns the newly created item</response>
    /// <response code="400">If the item is null</response>
    [HttpPost]
    [ProducesResponseType(typeof(FornecedorDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BadRequestResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Criar(Models.CriarFornecedor model)
    {
        var criarFornecedor = new Domain.Models.CriarFornecedor(
            Nome:   model.Nome,
            Descricao: model.Descricao,
            CpfCnpj: model.CpfCnpj,
            Telefone: model.Telefone,
            Localizacao: new Domain.Models.EnderecoFornecedor(
                Rua: model.Localizacao.Rua,
                Bairro: model.Localizacao.Bairro,
                Numero: model.Localizacao.Numero,
                Cidade: model.Localizacao.Cidade,
                UF: model.Localizacao.UF,
                CEP: model.Localizacao.CEP
            ));

        var result = await _service.CriarFornecedor(criarFornecedor);
        if (result.IsFailed)
            return HandleFailedResult(result);
        return Ok(result.Value);

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

