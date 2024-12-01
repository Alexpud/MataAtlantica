namespace MataAtlantica.Domain.Models;

public class AdicionarImagemProdutoDto
{
    public AdicionarImagemProdutoDto(string produtoId, int ordem)
    {
        ProdutoId = produtoId;
        Ordem = ordem;
    }

    public int Ordem { get; set; }
    public string ProdutoId { get; set; }
}
