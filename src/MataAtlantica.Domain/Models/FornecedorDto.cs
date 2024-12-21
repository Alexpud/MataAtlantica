namespace MataAtlantica.Domain.Models;

public class FornecedorDto
{
    public string Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public EnderecoFornecedor Localizacao { get; set; }
    public string CpfCnpj { get; set; }
    public string Telefone { get; set; }
}