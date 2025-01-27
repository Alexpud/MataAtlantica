using MataAtlantica.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MataAtlantica.Infrastructure.Data.Configurations;

public class FornecedorEntityTypeConfiguration : IEntityTypeConfiguration<Fornecedor>
{
    public void Configure(EntityTypeBuilder<Fornecedor> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasMaxLength(36).IsRequired();
        builder.Property(p => p.Nome).HasMaxLength(100).IsRequired();
        builder.Property(p => p.Descricao).HasMaxLength(1000).IsRequired();
        builder.Property(p => p.CpfCnpj).HasMaxLength(100).IsRequired();
        builder.Property(p => p.Telefone).HasMaxLength(20).IsRequired();
        builder.Property(p => p.CriadoEm).HasColumnType("timestamp without time zone").IsRequired();
        builder.Property(p => p.UltimaAtualizacao).HasColumnType("timestamp without time zone").IsRequired();
        builder.OwnsOne(p => p.Localizacao, enderecoBuilder =>
        {
            enderecoBuilder.Property(nameof(Endereco.Cidade)).HasMaxLength(50);
            enderecoBuilder.Property(nameof(Endereco.Bairro)).HasMaxLength(50);
            enderecoBuilder.Property(nameof(Endereco.Numero)).HasMaxLength(10);
            enderecoBuilder.Property(nameof(Endereco.UF)).HasMaxLength(5);
        });
        //builder.OwnsOne(p => p.Localizacao);
        //builder.OwnsOne("Endereco", "Localizacao");
    }
}