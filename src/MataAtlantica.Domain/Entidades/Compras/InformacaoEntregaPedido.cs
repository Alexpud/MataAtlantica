namespace MataAtlantica.Domain.Entidades.Compras;

public class InformacaoEntregaPedido : EntidadeBase
{
    /// <summary>
    /// Custo da entrega do pedido
    /// </summary>
    public decimal Valor { get; set; }

    /// <summary>
    /// Data de entrega estimada para o pedido
    /// </summary>
    public DateTime? EntregaEstimada { get; set; }

    /// <summary>
    /// Nome da transportadora
    /// </summary>
    public string Transportadora { get; set; }

    /// <summary>
    /// Pedido de compra vinculado a entrega
    /// </summary>
    public PedidoCompra PedidoCompra { get; set; }
    
    public string PedidoCompraID { get; set; }
}