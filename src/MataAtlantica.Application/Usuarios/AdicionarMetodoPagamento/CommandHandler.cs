using FluentResults;
using FluentValidation;
using MataAtlantica.Domain.Entidades;
using MataAtlantica.Domain.Erros;
using MataAtlantica.Domain.Models.Usuarios;
using MataAtlantica.Domain.Services;
using MataAtlantica.Infrastructure.Abstract;
using MediatR;

namespace MataAtlantica.Application.Usuarios.AdicionarMetodoPagamento;

public record AdicionarMetodoPagamentoCommand(
    string UsuarioId,
    BandeiraCartao Bandeira,
    string NumeroIdentificacao,
    DateTime Validade,
    TipoPagamento Tipo) : IRequest<Result>;

public class AdicionarMetodoPagamentoCommandValidator : AbstractValidator<AdicionarMetodoPagamentoCommand>
{
    public AdicionarMetodoPagamentoCommandValidator()
    {
        RuleFor(P => P.NumeroIdentificacao)
            .NotEmpty()
            .WithErrorCode(nameof(BusinessErrors.NumeroIdentificacaoObrigatorioParaMetodoPagamento))
            .WithMessage(BusinessErrors.NumeroIdentificacaoObrigatorioParaMetodoPagamento.Message);
    }
}

public class CommandHandler(
    IValidadorCartao validadorCartao,
    UsuarioService usuarioService) : IRequestHandler<AdicionarMetodoPagamentoCommand, Result>
{
    private readonly UsuarioService _usuarioService = usuarioService;
    private readonly IValidadorCartao _validadorCartaoCredito = validadorCartao;
    public async Task<Result> Handle(AdicionarMetodoPagamentoCommand request, CancellationToken cancellationToken)
    {
        var dto = new AdicionarMetodoPagamentoDto(
            request.UsuarioId,
            request.Bandeira,
            request.NumeroIdentificacao,
            request.Validade,
            request.Tipo);

        var result = await _usuarioService.AdicionarMetodoPagamento(dto);
        if (result.IsFailed)
            return result;

        var eventoValidacaoCartao = new EventoValidacaoCartao(
            request.UsuarioId,
            request.Bandeira,
            request.NumeroIdentificacao,
            request.Validade,
            request.Tipo);

        await _validadorCartaoCredito.EnviarNotificaco(eventoValidacaoCartao);
        return result;
    }
}
