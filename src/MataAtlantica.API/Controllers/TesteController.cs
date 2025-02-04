using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace MataAtlantica.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TesteController : ControllerBase
{
    [HttpGet]
    [OutputCache(Duration = 60, PolicyName = "CustomPolicy")]
    public IActionResult TesteCache()
    {
        return Ok("Teste de cache" + new Random().Next());
    }
}
