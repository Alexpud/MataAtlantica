using FluentResults;
using MataAtlantica.Domain.Models;
using MataAtlantica.Domain.Services;
using MediatR;

namespace MataAtlantica.Application.Fornecedores.AlterarFornecedor;

public record AlterarFornecedorCommand(
    string Id,
    string Nome,
    string Descricao,
    string CpfCnpj,
    string Telefone,
    EnderecoFornecedor Localizacao) : IRequest<Result<FornecedorDto>>
{ }

public class CommandHandler(FornecedorService fornecedorService) : IRequestHandler<AlterarFornecedorCommand, Result<FornecedorDto>>
{
    private readonly FornecedorService _fornecedorService = fornecedorService;
    public async Task<Result<FornecedorDto>> Handle(AlterarFornecedorCommand request, CancellationToken cancellationToken)
    {
        var dto = new AlterarFornecedorDto(
            request.Id,
            request.Nome,
            request.Descricao,
            request.CpfCnpj,
            request.Telefone,
            request.Localizacao);

        return await _fornecedorService.Alterar(dto);
    }
}
