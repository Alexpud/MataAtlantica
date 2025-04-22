using Azure;
using MataAtlantica.Application.Common;
using MataAtlantica.Domain.Abstract.Services;
using MataAtlantica.Domain.Models.Compras;
using MediatR;

namespace MataAtlantica.Application.Compras.CriarPedido;

public class CommandHandler(IPagamentoService pagamentoService,
    IPedidoCompraService pedidoCompraService) : IRequestHandler<CriarPedidoCompraCommand, CommandResponse>
{
    private readonly IPagamentoService _pagamentoService = pagamentoService;
    private readonly IPedidoCompraService _pedidoCompraService = pedidoCompraService;

    public async Task<CommandResponse> Handle(CriarPedidoCompraCommand request, CancellationToken cancellationToken)
    {
        // Validar as informações de pagamento
        var response = new CommandResponse();
        
        var pedidoCompraDto = new PedidoCompraDto();
        var adicionarPedidoCompraResult = await _pedidoCompraService.Adicionar(pedidoCompraDto);
        if (adicionarPedidoCompraResult.IsFailed)
        {
           response.WithErrors(adicionarPedidoCompraResult.Errors);
           return response;
        }
        
        var pedidoCompra = adicionarPedidoCompraResult.Value;
        var informacaoPagamentoResult = await _pagamentoService.ValidarInformacoesPagamento(request.InformacaoPagamento);
        if (informacaoPagamentoResult.IsFailed)
        {
            // Atualizar a informação de pedido, informando o erro que deu de validação de pagamento
            response.WithErrors(informacaoPagamentoResult.Errors);
            return response;
        }
        // Realizar pedido aos fornecedores
        
        // Fazer ccmunicação com o meio de compra, avisando da compra
        
        // INserir o pedido como status de compra pendente
        
        // Lançar evento avisando da compra pendente
        throw new NotImplementedException();
    }
}