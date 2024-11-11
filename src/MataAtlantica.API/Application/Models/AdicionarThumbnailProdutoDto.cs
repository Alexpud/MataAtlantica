namespace MataAtlantica.API.Application.Models;

public class AdicionarThumbnailProdutoDto
{
    public string ProdutoId { get; set; }
    public IFormFile Thumbnail { get; set; }
    public int Ordem { get; set; }
}
