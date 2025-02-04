using FluentResults;
using FluentValidation;
using MataAtlantica.Domain.Entidades;
using MataAtlantica.Domain.Erros;
using MataAtlantica.Domain.Models.Usuarios;
using MataAtlantica.Domain.Services;
using MataAtlantica.Infrastructure.Abstract;
using MediatR;

namespace MataAtlantica.Application.Usuarios.AlterarMetodoPagamento;

public record AlterarMetodoPagamentoCommand(
    string UsuarioId,
    string MetodoPagamentoId,
    BandeiraCartao Bandeira,
    string NumeroIdentificacao,
    DateTime Validade,
    TipoPagamento Tipo) : IRequest<Result>;

public class AlterarMetododPagamentoCommandValidator : AbstractValidator<AlterarMetodoPagamentoCommand>
{
    public AlterarMetododPagamentoCommandValidator()
    {
        RuleFor(P => P.NumeroIdentificacao)
            .NotEmpty()
            .WithErrorCode(nameof(BusinessErrors.NumeroIdentificacaoObrigatorioParaMetodoPagamento))
            .WithMessage(BusinessErrors.NumeroIdentificacaoObrigatorioParaMetodoPagamento.Message)
            .CreditCard()
            .WithErrorCode(nameof(BusinessErrors.NumeroIdentificacaoInvalidoParaMetodoPagamento))
            .WithMessage(BusinessErrors.NumeroIdentificacaoInvalidoParaMetodoPagamento.Message);

        RuleFor(p => p.UsuarioId)
            .NotEmpty()
            .WithErrorCode(nameof(BusinessErrors.IdUsuarioObrigatorio))
            .WithMessage(BusinessErrors.IdUsuarioObrigatorio.Message);

        RuleFor(p => p.MetodoPagamentoId)
            .NotEmpty()
            .WithErrorCode(nameof(BusinessErrors.IdMetodoPagamentoObrigatorio))
            .WithMessage(BusinessErrors.IdMetodoPagamentoObrigatorio.Message);

    }
}

public class CommandHandler(
    IValidadorCartao validadorCartao,
    UsuarioService usuarioService) : IRequestHandler<AlterarMetodoPagamentoCommand, Result>
{
    private readonly IValidadorCartao _validadorCartao = validadorCartao;
    private readonly UsuarioService _usuarioService = usuarioService;

    public async Task<Result> Handle(AlterarMetodoPagamentoCommand request, CancellationToken cancellationToken)
    {
        var dto = new AlterarMetodoPagamentoDto(
            request.MetodoPagamentoId,
            request.UsuarioId,
            request.Bandeira,
            request.NumeroIdentificacao,
            request.Validade,
            request.Tipo);

        var result = await _usuarioService.AlterarMetodoPagamento(dto);
        if (result.IsFailed)
            return result;
        var eventoValidacaoCartao = new EventoValidacaoCartao(
            request.UsuarioId,
            request.Bandeira,
            request.NumeroIdentificacao,
            request.Validade,
            request.Tipo);

        await _validadorCartao.EnviarNotificaco(eventoValidacaoCartao);
        return result;
    }
}
