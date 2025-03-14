using FluentValidation;
using MataAtlantica.Domain.Abstract.Repositories;
using MataAtlantica.Domain.Entidades;
using MataAtlantica.Domain.Erros;

namespace MataAtlantica.Domain.Models.Usuarios;

public record AlterarMetodoPagamentoDto(
    string MetodoPagamentoId,
    string UsuarioId,
    BandeiraCartao Bandeira,
    string NumeroIdentificacao,
    DateTime Validade,
    TipoPagamento Tipo);


public class AlterarMetodoPagamentoDtoValidator : AbstractValidator<AlterarMetodoPagamentoDto>
{
    private readonly IUsuarioRepository _usuarioRepository;
    public AlterarMetodoPagamentoDtoValidator(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;

        RuleFor(p => p.UsuarioId)
            .MustAsync(async (usuarioId, _) => await _usuarioRepository.ObterPorId(usuarioId) != null)
            .WithErrorCode(nameof(BusinessErrors.UsuarioNaoEncontrado))
            .WithMessage(BusinessErrors.UsuarioNaoEncontrado.Message);

        RuleFor(p => p.MetodoPagamentoId)
            .MustAsync(async (dto, metodoPagamentoId, _) => await PossuiMetodoPagamento(dto.UsuarioId, dto.MetodoPagamentoId))
            .WithErrorCode(nameof(BusinessErrors.MetodoPagamentoNaoEncontrado))
            .WithMessage(BusinessErrors.MetodoPagamentoNaoEncontrado.Message);

        RuleFor(p => p.NumeroIdentificacao)
            .CreditCard()
            .WithErrorCode(nameof(BusinessErrors.NumeroIdentificacaoInvalidoParaMetodoPagamento))
            .WithMessage(BusinessErrors.NumeroIdentificacaoInvalidoParaMetodoPagamento.Message);

        RuleFor(p => p.Validade)
            .GreaterThan(DateTime.Now)
            .WithErrorCode(nameof(BusinessErrors.ValidadeInferiorADataAtualParaMetodoPagamento))
            .WithMessage(BusinessErrors.ValidadeInferiorADataAtualParaMetodoPagamento.Message);
    }

    private async Task<bool> PossuiMetodoPagamento(string usuarioId, string metodoPagamentoId)
    {
        var usuario = await _usuarioRepository.ObterPorId(usuarioId, usuario => usuario.OpcoesPagamento);
        if (usuario == null) return false;
        return usuario.OpcoesPagamento.FirstOrDefault(p => p.Id == metodoPagamentoId) != null;
    }
}