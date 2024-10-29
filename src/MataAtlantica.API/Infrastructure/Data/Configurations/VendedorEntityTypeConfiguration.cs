using MataAtlantica.API.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MataAtlantica.API.Infrastructure.Data.Configurations;

public class VendedorEntityTypeConfiguration : IEntityTypeConfiguration<Fornecedor>
{
    public void Configure(EntityTypeBuilder<Fornecedor> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasMaxLength(36).IsRequired();
        builder.Property(p => p.Nome).HasMaxLength(100).IsRequired();
        builder.Property(p => p.Descricao).HasMaxLength(1000).IsRequired();
        builder.Property(p => p.CpfCnpj).HasMaxLength(100).IsRequired();
        builder.OwnsOne(p => p.Endereco, config =>
        {
            config.ToJson();
        });
    }
}