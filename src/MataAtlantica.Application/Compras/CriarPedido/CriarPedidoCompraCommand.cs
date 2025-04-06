using FluentValidation.Results;
using MataAtlantica.Application.Common;
using MataAtlantica.Domain.Models.Compras;
using MediatR;

namespace MataAtlantica.Application.Compras.CriarPedido;

public record CriarPedidoCompraCommand : BaseCommand, IRequest<CommandResponse>, IRequest
{
    public List<PedidoItemDto> Items { get; set; }

    public InformacaoPagamentoDto InformacaoPagamento { get; set; }

    public string UsuarioId { get; set; }

    public InformacaoEntregaDto InformacaoEntrega { get; set; }

    public override ValidationResult Validate()
    {
        return new CriarPedidoCompraCommandValidator().Validate(this);
    }
}