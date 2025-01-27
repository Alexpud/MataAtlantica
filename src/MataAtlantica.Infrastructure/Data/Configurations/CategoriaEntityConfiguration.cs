using MataAtlantica.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MataAtlantica.Infrastructure.Data.Configurations;

public class CategoriaEntityConfiguration : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasMaxLength(36).IsRequired();
        builder.Property(p => p.CriadoEm).IsRequired();
        builder.Property(p => p.Nome).HasMaxLength(50).IsRequired();
        builder.Property(p => p.CriadoEm).HasColumnType("timestamp without time zone").IsRequired();


        builder
            .HasMany(p => p.SubCategorias)
            .WithOne(p => p.CategoriaPai)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .IsRequired(false);
    }
}
