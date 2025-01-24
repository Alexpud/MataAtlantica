using System.ComponentModel.DataAnnotations;

namespace MataAtlantica.API.Models.Fornecedor;

public class AdicionarFornecedorRequest
{
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

    public Endereco Localizacao { get; set; }
}
