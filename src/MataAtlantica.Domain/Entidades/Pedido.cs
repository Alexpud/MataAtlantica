namespace MataAtlantica.Domain.Entidades;

public class Pedido : EntidadeBase
{
    public StatusPedido Status { get; private set; }
    public string Codigo { get; private set; }
    public IEnumerable<PedidoItem> Items { get; private set; } = Enumerable.Empty<PedidoItem>();
    public Endereco EnderecoEntrega { get; private set; }
    public PagamentoPedido Pagamento { get; set; }
    public Usuario Usuario { get; private set; }
    public string UsuarioId { get; private set; }
    public DateTime UltimaAtualizacao { get; private set; }

    private Pedido() { }

    public Pedido(string usuarioId, Endereco endereco)
    {
        Codigo = Guid.NewGuid().ToString("D");
        UsuarioId = usuarioId;
        EnderecoEntrega = endereco;
    }

    public void AlterarStatus(StatusPedido status)
        => Status = status;

    public void AdicionarItem(PedidoItem item)
        => Items = Items.Append(item);
}

public class PagamentoPedido
{
    public Pedido Pedido { get; set; }
    public string PedidoId { get; set; }
    public TipoPagamento Tipo { get; set; }
    public decimal Valor { get; set; }
    /// <summary>
    /// Representa a informacao de pagamento, como numero do cartao, data de validade, etc.
    /// </summary>
    public string InformacoesPagamento { get; set; }
}

public enum StatusPedido
{
    Pendente,
    Cancelado,
    Pago,
    Enviado
}

public class PedidoItem : EntidadeBase
{
    public int Quantidade { get; private set; }
    public decimal Valor { get; private set; }
    public Produto Produto { get; private set; }
    public string ProdutoId { get; private set; }
    public string PedidoCompraId { get; private set; }

    private PedidoItem() { }

    public PedidoItem(string pedidoCompraId, decimal valor, string produtoId, int quantidade)
    {
        PedidoCompraId = pedidoCompraId;
        ProdutoId = produtoId;
        Valor = valor;
        Quantidade = quantidade;
    }
}