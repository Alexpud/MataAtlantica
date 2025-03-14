using MataAtlantica.Application.Common;
using MataAtlantica.Domain.Models.Fornecedores;
using MataAtlantica.Domain.Services;
using MediatR;

namespace MataAtlantica.Application.Fornecedores.AlterarFornecedor;

public class CommandHandler(FornecedorService fornecedorService) : IRequestHandler<AlterarFornecedorCommand, CommandResponse<FornecedorDto>>
{
    private readonly FornecedorService _fornecedorService = fornecedorService;
    public async Task<CommandResponse<FornecedorDto>> Handle(AlterarFornecedorCommand request, CancellationToken cancellationToken)
    {
        var dto = new AlterarFornecedorDto(
            request.Id,
            request.Nome,
            request.Descricao,
            request.CpfCnpj,
            request.Telefone,
            request.Localizacao);

        var response = new CommandResponse<FornecedorDto>();
        var result = await _fornecedorService.Alterar(dto);
        if (result.IsFailed)
            response.WithErrors(result.Errors);
        else
            response.SetValue(result.Value);
        return response;
    }
}
