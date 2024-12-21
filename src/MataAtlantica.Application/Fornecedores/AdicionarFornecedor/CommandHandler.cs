using FluentResults;
using MataAtlantica.Domain.Models;
using MataAtlantica.Domain.Services;
using MediatR;

namespace MataAtlantica.Application.Fornecedores.AdicionarFornecedor;

public record AdicionarFornecedorCommand(
    string Nome,
    string Descricao,
    string CpfCnpj,
    string Telefone,
    EnderecoFornecedor Localizacao) : IRequest<Result<FornecedorDto>>;

internal class CommandHandler(FornecedorService fornecedorService) : IRequestHandler<AdicionarFornecedorCommand, Result<FornecedorDto>>
{
    private readonly FornecedorService _fornecedorService = fornecedorService;

    public async Task<Result<FornecedorDto>> Handle(AdicionarFornecedorCommand request, CancellationToken cancellationToken)
    {
        var dto = new AdicionarFornecedorDto(
            request.Nome,
            request.Descricao,
            request.CpfCnpj,
            request.Telefone,
            request.Localizacao);
        return await _fornecedorService.Adicionar(dto);
    }
}
