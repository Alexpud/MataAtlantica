using MataAtlantica.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MataAtlantica.Infrastructure.Data.Configurations;

public class AvaliacaoEntityTypeConfiguration : IEntityTypeConfiguration<Avaliacao>
{
    public void Configure(EntityTypeBuilder<Avaliacao> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasMaxLength(36).IsRequired();
        builder.Property(p => p.Descricao).HasMaxLength(1000).IsRequired();
        builder.Property(p => p.CriadoEm).HasColumnType("timestamp without time zone").IsRequired();
        builder.Property(p => p.UltimaAtualizacao).HasColumnType("timestamp without time zone").IsRequired();
        builder.Property(p => p.NotaProduto).IsRequired();
    }
}