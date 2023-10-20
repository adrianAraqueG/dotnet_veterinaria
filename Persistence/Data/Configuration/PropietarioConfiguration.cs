using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistence.Data.Configuration;
public class PropietarioConfiguration : IEntityTypeConfiguration<Propietario>
{
    public void Configure(EntityTypeBuilder<Propietario> builder)
    {
        {
            builder.ToTable("Propietarios");
            builder.Property(p => p.Nombre)
            .HasMaxLength(100)
            .IsRequired();
            builder.Property(p => p.Email)
            .HasMaxLength(150)
            .IsRequired();
            builder.HasIndex(p => p.Email)
            .IsUnique();
            builder.Property(p => p.Telefono)
            .HasMaxLength(100)
            .IsRequired();
        }
    }
}