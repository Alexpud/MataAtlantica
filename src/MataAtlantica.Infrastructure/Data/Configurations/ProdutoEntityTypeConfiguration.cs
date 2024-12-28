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

public class UsuarioEntityTypeConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Nome).HasMaxLength(50).IsRequired();
        builder.Property(p => p.Sobrenome).HasMaxLength(50).IsRequired();
        builder.Property(p => p.Login).HasMaxLength(100).IsRequired();
        
        builder.HasIndex(p => p.Login).IsUnique();

        builder.OwnsOne(p => p.Endereco, enderecoBuilder =>
        {
            enderecoBuilder.Property(nameof(Endereco.Rua)).HasMaxLength(100);
            enderecoBuilder.Property(nameof(Endereco.Cidade)).HasMaxLength(50);
            enderecoBuilder.Property(nameof(Endereco.Bairro)).HasMaxLength(50);
            enderecoBuilder.Property(nameof(Endereco.Numero)).HasMaxLength(10);
            enderecoBuilder.Property(nameof(Endereco.UF)).HasMaxLength(5);
        });
    }
}
