using FluentResults;
using MataAtlantica.Domain.Entidades.Compras;
using MataAtlantica.Domain.Models.Compras;

namespace MataAtlantica.Domain.Abstract.Services;

public interface IPedidoCompraService
{
    Task<Result<PedidoCompra>> Adicionar(PedidoCompraDto pedidoCompra);
}