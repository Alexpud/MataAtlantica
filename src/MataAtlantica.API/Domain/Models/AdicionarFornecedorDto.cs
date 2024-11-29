namespace MataAtlantica.API.Domain.Models;

public record class AdicionarFornecedorDto(string Nome, string Descricao, string CpfCnpj, string Telefone, EnderecoFornecedor Localizacao);

public record class EnderecoFornecedor(string Rua, string Bairro, string Numero, string Cidade, string UF, string CEP);