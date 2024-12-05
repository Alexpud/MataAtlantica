using MataAtlantica.Application.Produtos.CadastrarThumbnail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MataAtlantica.API.Presentation.Controllers;

[Route("api/mediatr")]
public class MediatrController(IMediator mediator) : BaseController
{
    private readonly IMediator _mediator = mediator;
    
    [HttpGet]
    public async Task<IActionResult> DoSomething()
    {
        await _mediator.Send(new AdicionarProdutoImagemCommand());
        return Ok();
    }
}
