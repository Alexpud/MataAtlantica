using FluentValidation;
using MataAtlantica.Domain.Abstract.Repositories;
using MataAtlantica.Domain.Entidades;
using MataAtlantica.Domain.Erros;

namespace MataAtlantica.Domain.Models.Usuarios;

public record AdicionarMetodoPagamentoDto(
    string UsuarioId, 
    BandeiraCartao Bandeira,
    string NumeroIdentificacao,
    DateTime Validade,
    TipoPagamento Tipo);


public class AdicionarMetodoPagamentoDtoValidator : AbstractValidator<AdicionarMetodoPagamentoDto>
{
    public AdicionarMetodoPagamentoDtoValidator(IUsuarioRepository usuarioRepository)
    {
        RuleFor(p => p.UsuarioId)
            .MustAsync(async (usuarioId, _) => await usuarioRepository.ObterPorId(usuarioId) != null)
            .WithErrorCode(nameof(BusinessErrors.UsuarioNaoEncontrado))
            .WithMessage(BusinessErrors.UsuarioNaoEncontrado.Message);

        RuleFor(p => p.NumeroIdentificacao)
            .CreditCard()
            .WithErrorCode(nameof(BusinessErrors.NumeroIdentificacaoInvalidoParaMetodoPagamento))
            .WithMessage(BusinessErrors.NumeroIdentificacaoInvalidoParaMetodoPagamento.Message);

        RuleFor(p => p.Validade)
            .GreaterThan(DateTime.Now)
            .WithErrorCode(nameof(BusinessErrors.ValidadeInferiorADataAtualParaMetodoPagamento))
            .WithMessage(BusinessErrors.ValidadeInferiorADataAtualParaMetodoPagamento.Message);
    }
}