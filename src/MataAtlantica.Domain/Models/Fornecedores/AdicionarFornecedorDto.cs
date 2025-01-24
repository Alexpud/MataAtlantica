namespace MataAtlantica.Domain.Models.Fornecedores;

public record class AdicionarFornecedorDto(string Nome, string Descricao, string CpfCnpj, string Telefone, Endereco Localizacao);
