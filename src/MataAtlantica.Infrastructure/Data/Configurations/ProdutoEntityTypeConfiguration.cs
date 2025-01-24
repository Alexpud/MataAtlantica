using MataAtlantica.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MataAtlantica.Infrastructure.Data.Configurations;

public class ProdutoEntityTypeConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasMaxLength(36).IsRequired();
        builder.Property(p => p.Nome).HasMaxLength(100).IsRequired();
        builder.Property(p => p.Descricao).HasMaxLength(500).IsRequired();
        builder.Property(p => p.Marca).HasMaxLength(100).IsRequired();
        builder.Property(p => p.Preco).IsRequired();

        builder
            .HasOne(p => p.Fornecedor)
            .WithMany(p => p.Produtos);

        builder.OwnsOne(p => p.ConfiguracaoImagens, config =>
        {
            config.ToJson();
            config.OwnsMany(p => p.Thumbnails);
            config.OwnsMany(p => p.ImagensIlustrativas);
        });
    }
}
