using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistence.Data.Configuration;
public class ProveedorConfiguration : IEntityTypeConfiguration<Proveedor>
{
    public void Configure(EntityTypeBuilder<Proveedor> builder)
    {
        {
            builder.ToTable("Proveedores");
            builder.Property(p => p.Nombre)
            .HasMaxLength(100)
            .IsRequired();
            builder.HasIndex(p => p.Nombre)
            .IsUnique();
            builder.Property(p => p.Direccion)
            .HasMaxLength(150)
            .IsRequired();
            builder.Property(p => p.Telefono)
            .HasMaxLength(15)
            .IsRequired();
        }
    }
}