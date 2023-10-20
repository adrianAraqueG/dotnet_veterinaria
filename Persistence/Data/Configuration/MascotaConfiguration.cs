using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistence.Data.Configuration;
public class MascotaConfiguration : IEntityTypeConfiguration<Mascota>
{
    public void Configure(EntityTypeBuilder<Mascota> builder)
    {
        {
            builder.ToTable("Mascotas");
            builder.Property(p => p.Nombre)
            .HasMaxLength(100)
            .IsRequired();

            builder.Property(p => p.FechaNacimiento)
            .IsRequired();

            builder.HasOne(p => p.Propietario)
            .WithMany(f => f.Mascotas)
            .HasForeignKey(fk => fk.IdPropietario)
            .IsRequired();
            builder.HasOne(p => p.Especie)
            .WithMany(f => f.Mascotas)
            .HasForeignKey(fk => fk.IdEspecie)
            .IsRequired();
            builder.HasOne(p => p.Raza)
            .WithMany(f => f.Mascotas)
            .HasForeignKey(fk => fk.IdRaza)
            .IsRequired();
        }
    }
}