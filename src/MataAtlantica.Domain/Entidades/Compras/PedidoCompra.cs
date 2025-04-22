namespace MataAtlantica.Domain.Entidades.Compras;

public class PedidoCompra : EntidadeBase
{
    /// <summary>
    /// Informa o status do pedido
    /// </summary>
    public StatusPedido Status { get; private set; }
    
    /// <summary>
    /// Codigo unico de identificação da compra
    /// </summary>
    public string Codigo { get; private set; }
    
    /// <summary>
    /// Items da compra
    /// </summary>
    public IEnumerable<PedidoItem> Items { get; private set; } = Enumerable.Empty<PedidoItem>();
    
    /// <summary>
    /// Informações de entrega do pedido
    /// </summary>
    public InformacaoEntregaPedido InformacaoEntregaPedido  { get; private set; }

    public string InformacaoEntregaId { get; private set; }
    
    /// <summary>
    /// Informacoes de pagamento da compra
    /// </summary>
    public InformacaoPagamentoPedido InformacaoPagamento { get; private set; }
    
    /// <summary>
    /// Usuario que realizou a compra
    /// </summary>
    public Usuario Usuario { get; private set; }
    public string UsuarioId { get; private set; }
    
    public DateTime? UltimaAtualizacao { get; private set; }

    /// <summary>
    /// Valor do pedido de compra
    /// </summary>
    public decimal Valor { get; private set; }

    private PedidoCompra() { }

    public PedidoCompra(string codigo, string usuarioId, decimal valor)
    {
        Status = StatusPedido.Pendente;
        Codigo = codigo;
        UsuarioId = usuarioId;
        Valor = valor;
    }

    public void AlterarStatus(StatusPedido status)
        => Status = status;

    public void AdicionarItem(PedidoItem item)
        => Items = Items.Append(item);
}

public enum StatusPedido
{
    Pendente,
    Cancelado,
    Pago,
    Enviado,
    Preparacao
}