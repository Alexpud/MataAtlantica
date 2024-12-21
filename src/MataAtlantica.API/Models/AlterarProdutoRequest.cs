using System.ComponentModel.DataAnnotations;

namespace MataAtlantica.API.Models;

public class AlterarProdutoRequest
{
    [Required]
    public string Nome { get; set; }
}
