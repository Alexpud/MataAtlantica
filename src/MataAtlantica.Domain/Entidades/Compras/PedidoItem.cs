namespace MataAtlantica.Domain.Entidades.Compras;

public class PedidoItem : EntidadeBase
{
    /// <summary>
    /// Valor do item do pedido
    /// </summary>
    public decimal Valor { get; private set; }
    
    /// <summary>
    /// Produto ao qual o item do pedido esta vinculado
    /// </summary>
    public Produto Produto { get; private set; }
    public string ProdutoId { get; private set; }
    
    /// <summary>
    /// Pedido de compra ao qual o item do pedido esta vinculado
    /// </summary>
    public string PedidoCompraId { get; private set; }

    public PedidoCompra PedidoCompra { get; set; }

    private PedidoItem() { }

    public PedidoItem(string pedidoCompraId, decimal valor, string produtoId)
    {
        PedidoCompraId = pedidoCompraId;
        ProdutoId = produtoId;
        Valor = valor;
    }
}