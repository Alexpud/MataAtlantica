//using MataAtlantica.Domain.Entidades;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace MataAtlantica.Infrastructure.Data.Configurations;
//public class PedidoEntityTypeConfiguration : IEntityTypeConfiguration<Pedido>
//{
//    public void Configure(EntityTypeBuilder<Pedido> builder)
//    {
//        builder.Property(p => p.Id).HasMaxLength(36).IsRequired();
//        builder.Property(p => p.Codigo).HasMaxLength(50).IsRequired();
//        builder.OwnsOne(p => p.EnderecoEntrega, enderecoBuilder =>
//        {
//            enderecoBuilder.Property(nameof(Endereco.Cidade)).HasMaxLength(50);
//            enderecoBuilder.Property(nameof(Endereco.Bairro)).HasMaxLength(50);
//            enderecoBuilder.Property(nameof(Endereco.Numero)).HasMaxLength(10);
//            enderecoBuilder.Property(nameof(Endereco.UF)).HasMaxLength(5);
//        });

//        builder.OwnsOne(p => p.Pagamento, pagamentoBuilder =>
//        {
//            pagamentoBuilder.ToTable("PagamentoPedido");
//            pagamentoBuilder.Property(nameof(PagamentoPedido.Valor)).IsRequired();
//            pagamentoBuilder.Property(nameof(PagamentoPedido.Tipo))
//                .HasConversion<string>()
//                .IsRequired();
//            //pagamentoBuilder.Property(nameof(PagamentoPedido.InformacoesPagamento))
//        });
//    }
//}
