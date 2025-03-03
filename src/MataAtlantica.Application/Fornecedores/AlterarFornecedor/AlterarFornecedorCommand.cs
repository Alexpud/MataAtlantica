using MataAtlantica.Application.Common;
using MataAtlantica.Domain.Models;
using MataAtlantica.Domain.Models.Fornecedores;
using MediatR;

namespace MataAtlantica.Application.Fornecedores.AlterarFornecedor;

public record AlterarFornecedorCommand(
    string Id,
    string Nome,
    string Descricao,
    string CpfCnpj,
    string Telefone,
    Endereco Localizacao) : BaseCommand, IRequest<CommandResponse<FornecedorDto>>
{ }
