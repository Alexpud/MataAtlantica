namespace MataAtlantica.API.Models.Produtos;

public class BuscarProdutosRequest
{
    /// <summary>
    /// Nome do produto a ser utilizado no filtro
    /// </summary>
    public string Nome { get; set; }
    /// <summary>
    /// Nome da categoria
    /// </summary>
    public string Categoria { get; set; }
    /// <summary>
    /// Nome do fornecedor 
    /// </summary>
    public string Fornecedor { get; set; }
}