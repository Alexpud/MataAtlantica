namespace MataAtlantica.Domain.Models.Fornecedores;

public class FornecedorDto
{
    public string Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public Endereco Localizacao { get; set; }
    public string CpfCnpj { get; set; }
    public string Telefone { get; set; }
}