using MataAtlantica.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MataAtlantica.Infrastructure.Data.Configurations;

public class UsuarioEntityTypeConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Nome).HasMaxLength(50).IsRequired();
        builder.Property(p => p.Sobrenome).HasMaxLength(50).IsRequired();
        builder.Property(p => p.Login).HasMaxLength(100).IsRequired();
        builder.Property(p => p.CriadoEm).HasColumnType("timestamp without time zone").IsRequired();
        builder.Property(p => p.UltimaAtualizacao).HasColumnType("timestamp without time zone").IsRequired();
        builder.HasIndex(p => p.Login).IsUnique();

        builder.OwnsOne(p => p.Endereco, enderecoBuilder =>
        {
            enderecoBuilder.Property(nameof(Endereco.Rua)).HasMaxLength(100);
            enderecoBuilder.Property(nameof(Endereco.Cidade)).HasMaxLength(50);
            enderecoBuilder.Property(nameof(Endereco.Bairro)).HasMaxLength(50);
            enderecoBuilder.Property(nameof(Endereco.Numero)).HasMaxLength(10);
            enderecoBuilder.Property(nameof(Endereco.UF)).HasMaxLength(5);
        });

        builder.OwnsMany(p => p.OpcoesPagamento, opcoesPagamentoBuilder =>
        {
            opcoesPagamentoBuilder.ToTable("MetodoPagamento");
            opcoesPagamentoBuilder.HasKey(nameof(MetodoPagamento.Id));
            opcoesPagamentoBuilder.Property(p => p.Bandeira)
                .HasConversion<string>()
                .IsRequired();

            opcoesPagamentoBuilder.Property(p => p.Tipo)
                .HasConversion<string>()
                .IsRequired();

            opcoesPagamentoBuilder.Property(p => p.NumeroIdentificacao)
                .HasMaxLength(16)
                .IsRequired();
        });
    }
}