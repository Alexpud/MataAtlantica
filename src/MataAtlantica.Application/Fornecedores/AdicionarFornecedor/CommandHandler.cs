using FluentResults;
using FluentValidation;
using MataAtlantica.Application.Common;
using MataAtlantica.Domain.Models.Fornecedores;
using MataAtlantica.Domain.Services;
using MediatR;

namespace MataAtlantica.Application.Fornecedores.AdicionarFornecedor;


internal class CommandHandler(FornecedorService fornecedorService) : IRequestHandler<AdicionarFornecedorCommand, CommandResponse<FornecedorDto>>
{
    private readonly FornecedorService _fornecedorService = fornecedorService;

    public async Task<CommandResponse<FornecedorDto>> Handle(AdicionarFornecedorCommand request, CancellationToken cancellationToken)
    {
        var response = new CommandResponse<FornecedorDto>();
        var dto = new AdicionarFornecedorDto(
            request.Nome,
            request.Descricao,
            request.CpfCnpj,
            request.Telefone,
            request.Localizacao);
        var result = await _fornecedorService.Adicionar(dto);
        if (result.IsFailed)
            response.WithErrors(result.Errors);
        else
            response.SetValue(result.Value);
        return response;
    }
}
