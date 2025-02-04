using FluentResults;
using FluentValidation;
using MataAtlantica.Domain.Erros;
using MataAtlantica.Domain.Models.Produtos;
using MataAtlantica.Domain.Services;
using MediatR;

namespace MataAtlantica.Application.Produtos.AdicionarProduto;

public record AdicionarProdutoCommand(
    string Nome,
    string CategoriaId,
    float Preco,
    string Descricao,
    string FornecedorId,
    string Marca
    ) : IRequest<Result<ProdutoDto>>
{
}

public class AdicionarProdutoCommandValidtor : AbstractValidator<AdicionarProdutoCommand>
{
    public AdicionarProdutoCommandValidtor()
    {
        RuleFor(produto => produto.Nome)
            .NotEmpty()
            .WithErrorCode(nameof(EntityValidationErrors.NomeObrigatorioParaProduto))
            .WithMessage(EntityValidationErrors.NomeObrigatorioParaProduto.Message);

        RuleFor(produto => produto.Descricao)
            .NotEmpty()
            .WithErrorCode(nameof(EntityValidationErrors.DescricaoObrigatorioParaProduto))
            .WithMessage(EntityValidationErrors.DescricaoObrigatorioParaProduto.Message);

        RuleFor(produto => produto.Marca)
            .NotEmpty()
            .WithErrorCode(nameof(EntityValidationErrors.MarcaObrigatoriaParaProduto))
            .WithMessage(EntityValidationErrors.MarcaObrigatoriaParaProduto.Message);

        RuleFor(produto => produto.Preco)
            .GreaterThan(0)
            .WithErrorCode(nameof(EntityValidationErrors.PrecoObrigatorioParaProduto))
            .WithMessage(EntityValidationErrors.PrecoObrigatorioParaProduto.Message);
    }
}

public class CommandHandler(ProdutoService produtoService) : IRequestHandler<AdicionarProdutoCommand, Result<ProdutoDto>>
{
    private readonly ProdutoService _produtoService = produtoService;
    public async Task<Result<ProdutoDto>> Handle(AdicionarProdutoCommand request, CancellationToken cancellationToken)
    {
        var dto = new AdicionarProdutoDto(
            request.Nome,
            request.CategoriaId,
            request.Preco,
            request.Descricao,
            request.FornecedorId,
            request.Marca);

        return await _produtoService.Adicionar(dto);
    }
}
