using FluentResults;
using FluentValidation;
using MataAtlantica.Domain.Erros;
using MataAtlantica.Domain.Models.Produtos;
using MataAtlantica.Domain.Services;
using MediatR;

namespace MataAtlantica.Application.Produtos.AlterarProduto;

public record AlterarProdutoCommand(string ProdutoId, string Nome) : IRequest<Result<ProdutoDto>> { }

public class AlterarProdutoCommandValidator  : AbstractValidator<AlterarProdutoCommand>
{
    public AlterarProdutoCommandValidator()
    {

        RuleFor(model => model.ProdutoId)
            .NotEmpty()
            .WithErrorCode(nameof(BusinessErrors.ProdutoNaoEncontrado))
            .WithMessage(BusinessErrors.ProdutoNaoEncontrado.Message);

        RuleFor(model => model.Nome)
            .NotEmpty()
            .WithErrorCode(nameof(EntityValidationErrors.NomeObrigatorioParaProduto))
            .WithMessage(EntityValidationErrors.NomeObrigatorioParaProduto.Message);
    }
}

public class CommandHandler(ProdutoService produtoService) : IRequestHandler<AlterarProdutoCommand, Result<ProdutoDto>>
{
    private readonly ProdutoService _produtoService = produtoService;
    public async Task<Result<ProdutoDto>> Handle(AlterarProdutoCommand command, CancellationToken cancellationToken)
    {
        var dto = new AlterarProdutoDto(command.ProdutoId, command.Nome);
        return await _produtoService.Alterar(dto);
    }
}
