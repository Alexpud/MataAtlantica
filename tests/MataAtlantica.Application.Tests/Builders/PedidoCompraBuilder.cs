using AutoFixture;
using MataAtlantica.Domain.Entidades.Compras;

namespace MataAtlantica.Application.Tests.Builders;

public class PedidoCompraBuilder : BaseBuilder<PedidoCompra, PedidoCompraBuilder>
{
    private readonly Fixture _fixture = new();

    public override PedidoCompraBuilder BuildDefault()
    {
        Object = new PedidoCompra(
            codigo: _fixture.Create<string>(),
            usuarioId: _fixture.Create<string>(),
            valor: _fixture.Create<decimal>());
        return this;
    }
}
