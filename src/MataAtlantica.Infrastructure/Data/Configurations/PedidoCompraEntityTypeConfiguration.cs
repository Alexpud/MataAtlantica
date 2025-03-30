using MataAtlantica.Domain.Entidades;
using MataAtlantica.Domain.Entidades.Compras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MataAtlantica.Infrastructure.Data.Configurations;

public class PedidoCompraEntityTypeConfiguration : IEntityTypeConfiguration<PedidoCompra>
{
    public void Configure(EntityTypeBuilder<PedidoCompra> builder)
    {
        builder.HasMany(p => p.Items)
            .WithOne(p => p.PedidoCompra)
            .IsRequired(true);

        builder.Property(p => p.Status)
            .HasConversion(p => p.ToString(), p => (StatusPedido)Enum.Parse(typeof(StatusPedido), p))
            .HasMaxLength(50);

        builder.Property(p => p.Codigo)
            .HasMaxLength(100)
            .IsRequired();
    }
}

public class InformacaoPagamentoPedidoEntityTypeConfiguration : IEntityTypeConfiguration<InformacaoPagamentoPedido>
{
    public void Configure(EntityTypeBuilder<InformacaoPagamentoPedido> builder)
    {
        builder.Property(p => p.Tipo)
            .HasConversion(p => p.ToString(), p => (TipoPagamento)Enum.Parse(typeof(TipoPagamento), p))
            .HasMaxLength(50);
    }
}

public class InformacaoEntregaPedidoEntityTypeConfiguration : IEntityTypeConfiguration<InformacaoEntregaPedido>
{
    public void Configure(EntityTypeBuilder<InformacaoEntregaPedido> builder)
    {

    }
}

public class PedidoItemEntityTypeConfiguration : IEntityTypeConfiguration<PedidoItem>
{
    public void Configure(EntityTypeBuilder<PedidoItem> builder)
    {
 
    }
}