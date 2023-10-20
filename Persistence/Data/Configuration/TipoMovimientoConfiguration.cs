using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistence.Data.Configuration;
public class TipoMovimientoConfiguration : IEntityTypeConfiguration<TipoMovimiento>
{
    public void Configure(EntityTypeBuilder<TipoMovimiento> builder)
    {
        {
            builder.ToTable("TipoMovimientos");
            builder.Property(p => p.Nombre)
            .HasMaxLength(100)
            .IsRequired();
            builder.HasIndex(p => p.Nombre)
            .IsUnique();
        }
    }
}