using FluentValidation;
using FluentValidation.Results;
using MataAtlantica.Domain.Erros;

namespace MataAtlantica.Domain.Entidades;

public class Avaliacao : EntidadeBase
{
    public string Descricao { get; set; }
    public float NotaProduto { get; set; }
    public string ProdutoId { get; set; }
    public Produto Produto { get; set; }
    public DateTime? UltimaAtualizacao { get; private set; }

}

public class AvaliacaoValidator : AbstractValidator<Avaliacao>
{
    public AvaliacaoValidator()
    {
        RuleFor(p => p.Descricao)
            .NotEmpty()
            .WithErrorCode(nameof(EntityValidationErrors.DescricaoObrigatorioParaAvaliacao))
            .WithMessage(EntityValidationErrors.DescricaoObrigatorioParaAvaliacao.Message);

        RuleFor(p => p.NotaProduto)
            .NotEmpty()
            .WithErrorCode(nameof(EntityValidationErrors.NotaProdutoObrigatoriaParaAvaliacao))
            .WithMessage(EntityValidationErrors.NotaProdutoObrigatoriaParaAvaliacao.Message);

        RuleFor(p => p.ProdutoId)
            .NotEmpty()
            .WithErrorCode(nameof(EntityValidationErrors.ProdutoObrigatorioParaAvaliacao))
            .WithMessage(EntityValidationErrors.ProdutoObrigatorioParaAvaliacao.Message);
    }
}