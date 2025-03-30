using MataAtlantica.Domain.Entidades;

namespace MataAtlantica.Domain.Models.Compras;

public class PedidoItemDto
{
    public decimal Valor { get; set; }
    public string ProdutoId { get; set; }
}

public class InformacaoPagamentoDto
{
    public string VoucherId { get; set; }
    public decimal ValorPago { get; set; }
    public decimal ValorTotal { get; set; }
    public TipoPagamento TipoPagamento { get; set; }
    public string Informacoes { get; set; }
}

public class InformacaoEntregaDto
{
    public Endereco Endereco { get; set; }
    
    /// <summary>
    /// Id de identificacao da transportadora externo
    /// </summary>
    public string TransportadoraExternalId { get; set; }

    /// <summary>
    /// Data estimada de entrega
    /// </summary>
    public DateTime? EntregaEstimada { get; set; }
}