using MataAtlantica.Domain.Entidades;

namespace MataAtlantica.Infrastructure.Abstract;

public interface IValidadorCartao
{
    Task EnviarNotificaco(EventoValidacaoCartao evento);
}

public class ValidadorCartao : IValidadorCartao
{
    /// <summary>
    /// Seria uma ação assincrona no futuro, mandando para um serviço de fila
    /// </summary>
    /// <param name="evento"></param>
    /// <returns></returns>
    public Task EnviarNotificaco(EventoValidacaoCartao evento)
    {
        // Enviar notificação para o serviço de validação de cartão
        return Task.CompletedTask;
    }
}

public record EventoValidacaoCartao(
    string UsuarioId,
    BandeiraCartao Bandeira,
    string NumeroIdentificacao,
    DateTime Validade,
    TipoPagamento Tipo);