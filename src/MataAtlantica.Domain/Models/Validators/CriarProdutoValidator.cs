using FluentValidation;
using MataAtlantica.Domain.Abstract.Repositories;
using MataAtlantica.Domain.Erros;
using MataAtlantica.Domain.Models;

namespace MataAtlantica.Domain.Models.Validators;

public class CriarProdutoValidator : AbstractValidator<AdicionarProdutoDto>
{
    public CriarProdutoValidator(ICategoriaRepository categoriaRepository, IFornecedorRepository fornecedorRepository)
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

        RuleFor(produto => produto.CategoriaId)
            .NotEmpty()
            .WithErrorCode(nameof(EntityValidationErrors.CategoriaObrigatorioParaProduto))
            .WithMessage(EntityValidationErrors.CategoriaObrigatorioParaProduto.Message)
            .MustAsync(async (categoriaId, cancellationToken) => await categoriaRepository.ObterPorId(categoriaId) != null)
            .WithErrorCode(nameof(BusinessErrors.CategoriaNaoEncontrada))
            .WithMessage(BusinessErrors.CategoriaNaoEncontrada.Message);

        RuleFor(produto => produto.FornecedorId)
            .NotEmpty()
            .WithErrorCode(nameof(EntityValidationErrors.FornecedorObrigatorioParaProduto))
            .WithMessage(EntityValidationErrors.FornecedorObrigatorioParaProduto.Message)
            .MustAsync(async (fornecedorId, cancellationToken) => await fornecedorRepository.ObterPorId(fornecedorId) != null)
            .WithErrorCode(nameof(BusinessErrors.FornecedorNaoEncontrado))
            .WithMessage(BusinessErrors.FornecedorNaoEncontrado.Message); ;
    }
}