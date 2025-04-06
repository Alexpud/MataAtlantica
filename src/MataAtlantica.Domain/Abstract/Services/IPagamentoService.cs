using FluentResults;
using MataAtlantica.Domain.Models.Compras;

namespace MataAtlantica.Domain.Abstract.Services;

public interface IPagamentoService
{
    Task<Result<bool>> ValidarInformacoesPagamento(InformacaoPagamentoDto dto);
}