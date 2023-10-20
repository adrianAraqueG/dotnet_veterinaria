using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistence.Data.Configuration;
public class VeterinarioConfiguration : IEntityTypeConfiguration<Veterinario>
{
    public void Configure(EntityTypeBuilder<Veterinario> builder)
    {
        {
            builder.ToTable("Veterinarios");
            builder.Property(p => p.Nombre)
            .HasMaxLength(100)
            .IsRequired();
            builder.Property(p => p.Telefono)
            .HasMaxLength(15)
            .IsRequired();
            builder.Property(p => p.Especialidad)
            .HasMaxLength(200)
            .IsRequired();
            builder.HasOne(p => p.Usuario)
            .WithMany(f => f.Veterinarios)
            .HasForeignKey(fk => fk.IdUsuario)
            .IsRequired();
        }
    }
}