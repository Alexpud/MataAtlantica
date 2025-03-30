namespace MataAtlantica.Domain.Entidades.Compras;

public class InformacaoPagamentoPedido : EntidadeBase
{
    public PedidoCompra PedidoCompra { get; set; }
    public string PedidoCompraId { get; set; }
    
    /// <summary>
    /// Informa o tipo de pagamento: credito ou debito
    /// </summary>
    public TipoPagamento Tipo { get; private set; }
    
    /// <summary>
    /// Valor da compra pago
    /// </summary>
    public decimal Valor { get; private set; }
    
    /// <summary>
    /// Valor total da compra com desconto
    /// </summary>
    public decimal ValorTotal { get; private set; }

    /// <summary>
    /// Informa o status do pagamento do pedido
    /// </summary>
    public StatusPagamento Status { get; private set; }
    
    // public Voucher Voucher { get; set; }
    // public string VoucherId { get; set; }
    
    /// <summary>
    /// Representa a informacao de pagamento, como numero do cartao, data de validade, etc.
    /// </summary>
    public string InformacoesPagamento { get; private set; }
}

public enum StatusPagamento
{
    Aprovado,
    Pendente,
    Negado,
    EmAndamento
}