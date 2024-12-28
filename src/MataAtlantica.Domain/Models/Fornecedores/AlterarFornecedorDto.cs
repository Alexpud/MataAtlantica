using System.ComponentModel.DataAnnotations;

namespace MataAtlantica.Domain.Models.Fornecedores;

public record class AlterarFornecedorDto(string Id, string Nome, string Descricao, string CpfCnpj, string Telefone, Endereco Localizacao);