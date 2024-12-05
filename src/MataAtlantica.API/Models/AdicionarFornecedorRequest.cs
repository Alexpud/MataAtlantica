using System.ComponentModel.DataAnnotations;

namespace MataAtlantica.API.Models;

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

    public EnderecoFornecedor Localizacao { get; set; }
}

public class EnderecoFornecedor
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