using MataAtlantica.API.Models;
using MataAtlantica.Application.Fornecedores.AdicionarFornecedor;
using MataAtlantica.Application.Fornecedores.Listar;
using MataAtlantica.Application.Fornecedores.ObterPorId;
using MataAtlantica.Domain.Models;
using MataAtlantica.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MataAtlantica.API.Controllers;

[Route("api/[controller]")]
[Produces("application/json")]
public class FornecedoresController(IMediator mediator, FornecedorService service) : BaseController
{
    private readonly FornecedorService _service = service;
    private readonly IMediator _mediator = mediator;

    /// <summary>
    /// Obtem fornecedor pelo Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(string id)
    {
        var fornecedor = await _mediator.Send(new ObterFornecedorPorIdQuery(id));
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
    [HttpPost]
    [ProducesResponseType(typeof(FornecedorDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BadRequestResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Criar(AdicionarFornecedorRequest model)
    {
        var command = new AdicionarFornecedorCommand(
            Nome: model.Nome,
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

        var result = await _mediator.Send(command);
        if (result.IsFailed)
            return HandleFailedResult(result);
        return Ok(result.Value);

    }

    /// <summary>
    /// Altera um fornecedor
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <remarks>
    /// Exemplo de requisicao:
    /// 
    ///     PUT /Fornecedores/{id}
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
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(FornecedorDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BadRequestResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Alterar(string id, AlterarFornecedorRequest model)
    {
        var alterarFornecedor = new AlterarFornecedorDto(
            Id: id,
            Nome: model.Nome,
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
        var result = await _service.Alterar(alterarFornecedor);
        if (result.IsFailed)
            return HandleFailedResult(result);
        return Ok(result.Value);
    }

    /// <summary>
    /// Lista os fornecedores
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<FornecedorDto>), (int)HttpStatusCode.OK)]
    public IActionResult Listar()
    {
        return Ok(_mediator.Send(new ListarFornecedoresQuery()));
    }
}

