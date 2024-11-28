using MataAtlantica.API.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace MataAtlantica.API.Presentation.Controllers;

public partial class ProdutoImagensController(ImagensProdutoService imagemProdutoService) : BaseController
{
    private readonly ImagensProdutoService _imagemProdutoService = imagemProdutoService;

    /// <summary>
    /// Adiciona uma imagem thumbnail na ordem especificada. Se uma imagem ja existir nessa ordem, ela e sobreescrita
    /// </summary>
    /// <param name="request"></param>
    /// <param name="produtoId"></param>
    /// <returns></returns>
    [Consumes("multipart/form-data")]
    [HttpPost("api/produtos/{produtoId}/thumbnail")]
    public async Task<IActionResult> AdicionarThumbnail([FromForm] AdicionarImagemRequest request, string produtoId)
    {
        var model = new Application.Models.AdicionarImagemProdutoDto
        {
            Ordem = request.Ordem,
            ProdutoId = produtoId,
            ArquivoImagem = request.File
        };
        var result = await _imagemProdutoService.AdicionarThumbnail(model);
        if (result.IsFailed)
            return HandleFailedResult(result);
        return Ok();
    }

    [Consumes("multipart/form-data")]
    [HttpPost("api/produtos/{produtoId}/imagem-ilustrativa")]
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
