using MataAtlantica.API.Controllers;
using MataAtlantica.Application.Produtos.AdicionarImagemIlustrativa;
using MataAtlantica.Application.Produtos.AdicionarThumbnail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MataAtlantica.API.Presentation.Controllers;

[Route("api/produtos")]
public partial class ProdutoImagensController(IMediator mediator) : BaseController
{
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
        var model = new AdicionarThumbnailCommand
        {
            Ordem = request.Ordem,
            ProdutoId = produtoId,
            ArquivoImagem = request.File
        };
        return HandleResult(await _mediator.Send(model));
    }

    [Consumes("multipart/form-data")]
    [HttpPost("{produtoId}/imagem-ilustrativa")]
    public async Task<IActionResult> AdicionarImagemIlustrativa([FromForm] AdicionarImagemRequest request, string produtoId)
    {
        var model = new AdicionarImagemIlustrativaCommand
        {
            Ordem = request.Ordem,
            ProdutoId = produtoId,
            ArquivoImagem = request.File
        };
        return HandleResult(await _mediator.Send(model));
    }
}
