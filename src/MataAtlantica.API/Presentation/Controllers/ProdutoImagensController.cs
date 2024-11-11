using MataAtlantica.API.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace MataAtlantica.API.Presentation.Controllers;

public partial class ProdutoImagensController(ImagensProdutoService imagemProdutoService) : BaseController
{
    private readonly ImagensProdutoService _imagemProdutoService = imagemProdutoService;

    [Consumes("multipart/form-data")]
    [HttpPost("api/produtos/{produtoId}/thumbnail")]
    public async Task<IActionResult> AdicionarThumbnail([FromForm] AdicionarThumbnailRequest request, string produtoId)
    {
        var model = new Application.Models.AdicionarThumbnailProdutoDto
        {
            Ordem = request.Ordem,
            ProdutoId = produtoId,
            Thumbnail = request.File
        };
        await _imagemProdutoService.AdicionarThumbnails(model);
        return Ok();
    }
}
