﻿using MataAtlantica.API.Application.Services;
using MataAtlantica.API.Controllers;
using MataAtlantica.Application.Produtos.AdicionarThumbnail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MataAtlantica.API.Presentation.Controllers;

[Route("api/produtos")]
public partial class ProdutoImagensController(ImagensProdutoService imagemProdutoService,
    IMediator mediator) : BaseController
{
    private readonly ImagensProdutoService _imagemProdutoService = imagemProdutoService;
    private readonly IMediator _mediator = mediator;

    /// <summary>
    /// Adiciona uma imagem thumbnail na ordem especificada. Se uma imagem ja existir nessa ordem, ela e sobreescrita
    /// </summary>
    /// <param name="request"></param>
    /// <param name="produtoId"></param>
    /// <returns></returns>
    [Consumes("multipart/form-data")]
    [HttpPost("{produtoId}/thumbnail")]
    public async Task<IActionResult> AdicionarThumbnail([FromForm] AdicionarImagemRequest request, string produtoId)
    {
        var model = new AdicionarProdutoThumbnailCommand
        {
            Ordem = request.Ordem,
            ProdutoId = produtoId,
            ArquivoImagem = request.File
        };
        var result = await _mediator.Send(model);
        if (result.IsFailed)
            return HandleFailedResult(result);
        return Ok();
    }

    [Consumes("multipart/form-data")]
    [HttpPost("{produtoId}/imagem-ilustrativa")]
    public async Task<IActionResult> AdicionarImagemIlustrativa([FromForm] AdicionarImagemRequest request, string produtoId)
    {
        var model = new Application.Models.AdicionarImagemProdutoDto
        {
            Ordem = request.Ordem,
            ProdutoId = produtoId,
            ArquivoImagem = request.File
        };
        var result = await _imagemProdutoService.AdicionarImagemIlustrativa(model);
        if (result.IsFailed)
            return HandleFailedResult(result);
        return Ok();
    }
}