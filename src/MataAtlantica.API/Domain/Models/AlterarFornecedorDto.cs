using System.ComponentModel.DataAnnotations;

namespace MataAtlantica.API.Domain.Models;

public record class AlterarFornecedorDto(string Id, string Nome, string Descricao, string CpfCnpj, string Telefone, EnderecoFornecedor Localizacao);