using System.ComponentModel.DataAnnotations;

namespace MataAtlantica.API.Presentation.Models;

public class AlterarFornecedor
{
    [Required]
    public string Id { get; set; }
    
    [Required]
    public string Nome { get; set; }

    [Required]
    public string Descricao { get; set; }
    
    /// <summary>
    /// Cpf/Cnpj do fornecedor sem formatacao
    /// </summary>
    [Required]
    public string CpfCnpj { get; set; }

    [Required]
    public string Telefone { get; set; }

    public EnderecoFornecedor Endereco { get; set; }
}