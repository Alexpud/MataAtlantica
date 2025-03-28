using System.ComponentModel.DataAnnotations;

namespace MataAtlantica.API.Models.Produtos;

public class AdicionarProdutoRequest
{
    /// <summary>
    /// Nome do produto
    /// </summary>
    [Required]
    public string Nome { get; set; }

    /// <summary>
    /// Id da categoria do produto
    /// </summary>
    [Required]
    public string CategoriaId { get; set; }

    /// <summary>
    /// Preco do produto
    /// </summary>
    [Required]
    [Range(0, double.MaxValue)]
    public float Preco { get; set; }

    /// <summary>
    /// Descricao do produto
    /// </summary>
    [Required]
    public string Descricao { get; set; }

    /// <summary>
    /// Id do fornecedor do produto
    /// </summary>
    [Required]
    public string FornecedorId { get; set; }

    /// <summary>
    /// Nome da marca do produto
    /// </summary>
    [Required]
    public string Marca { get; set; }
}