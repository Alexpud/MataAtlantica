using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using MataAtlantica.Application.Common;
using MataAtlantica.Domain.Models;
using MataAtlantica.Domain.Models.Fornecedores;
using MataAtlantica.Domain.Services;
using MediatR;

namespace MataAtlantica.Application.Fornecedores.AdicionarFornecedor;

public record AdicionarFornecedorCommand(
    string Nome,
    string Descricao,
    string CpfCnpj,
    string Telefone,
    Endereco Localizacao) : BaseCommand, IRequest<Result<FornecedorDto>>
{
    public override ValidationResult Validate()
    {
        var validator = new AdicionarFornecedorCommandValidator();
        return validator.Validate(this);
    }
}

public class AdicionarFornecedorCommandValidator : AbstractValidator<AdicionarFornecedorCommand>
{

}

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
