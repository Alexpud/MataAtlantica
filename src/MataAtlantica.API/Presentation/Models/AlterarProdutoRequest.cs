using System.ComponentModel.DataAnnotations;

namespace MataAtlantica.API.Presentation.Models;

public class AlterarProdutoRequest
{
    [Required]
    public string Nome { get; set; }
}
