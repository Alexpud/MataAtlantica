using FluentValidation.Results;
using MataAtlantica.Application.Common;
using MataAtlantica.Domain.Models;
using MataAtlantica.Domain.Models.Fornecedores;
using MediatR;

namespace MataAtlantica.Application.Fornecedores.AdicionarFornecedor;

public record AdicionarFornecedorCommand(
    string Nome,
    string Descricao,
    string CpfCnpj,
    string Telefone,
    Endereco Localizacao) : BaseCommand, IRequest<CommandResponse<FornecedorDto>>
{
}
