using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class LaboratorioConfiguration : IEntityTypeConfiguration<Laboratorio>
{
    public void Configure(EntityTypeBuilder<Laboratorio> builder)
    {
        {
            builder.ToTable("Laboratorios");
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