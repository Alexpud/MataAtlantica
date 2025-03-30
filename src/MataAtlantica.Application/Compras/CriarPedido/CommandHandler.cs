using MataAtlantica.Application.Common;
using MataAtlantica.Domain.Models.Compras;
using MediatR;

namespace MataAtlantica.Application.Compras.CriarPedido;

public record CriarPedidoCompraCommand : BaseCommand, IRequest
{
    public List<PedidoItemDto> Items { get; set; }

    public InformacaoPagamentoDto InformacaoPagamento { get; set; }

    public string UsuarioId { get; set; }

    public InformacaoEntregaDto InformacaoEntrega { get; set; }
}

public class CommandHandler : IRequestHandler<CriarPedidoCompraCommand>
{
    public Task Handle(CriarPedidoCompraCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}