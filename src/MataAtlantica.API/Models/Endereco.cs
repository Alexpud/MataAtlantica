using System.ComponentModel.DataAnnotations;

namespace MataAtlantica.API.Models;

public class Endereco
{
    [Required]
    public string Rua { get; set; }

    [Required]
    public string Bairro { get; set; }

    [Required]
    public string Numero { get; set; }

    [Required]
    public string Cidade { get; set; }

    /// <summary>
    /// Estado
    /// </summary>
    [Required]
    public string UF { get; set; }

    /// <summary>
    /// CEP do endereço sem formatacao
    /// </summary>
    [Required]
    public string CEP { get; set; }
}