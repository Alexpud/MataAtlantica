namespace MataAtlantica.API.Application.Models;

public class AdicionarImagemProdutoDto
{
    public string ProdutoId { get; set; }
    public IFormFile ArquivoImagem { get; set; }
    public int Ordem { get; set; }
}
