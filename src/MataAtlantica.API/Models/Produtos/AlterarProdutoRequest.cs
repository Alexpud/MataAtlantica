using System.ComponentModel.DataAnnotations;

namespace MataAtlantica.API.Models.Produtos;

public class AlterarProdutoRequest
{
    [Required]
    public string Nome { get; set; }
}
