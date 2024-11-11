namespace MataAtlantica.API.Domain.Models;

public class AdicionarThumbnailProdutoDto
{
    public AdicionarThumbnailProdutoDto(string produtoId, int ordem)
    {
        ProdutoId = produtoId;
        Ordem = ordem;
    }

    public int Ordem { get; set; }
    public string ProdutoId { get; set; }
}
