namespace MataAtlantica.API.Presentation.Models;

public class BuscarProdutosArgs
{
    /// <summary>
    /// Nome do produto a ser utilizado no filtro
    /// </summary>
    public string Nome { get; set; }
    /// <summary>
    /// Id da categoria a ser utilizado no filtro
    /// </summary>
    public string CategoriaId { get; set; }
    /// <summary>
    /// Id do vendedor a ser utilizado no filtro
    /// </summary>
    public string VendedorId { get; set; }
}