using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class CitaConfiguration : IEntityTypeConfiguration<Cita>
{
    public void Configure(EntityTypeBuilder<Cita> builder)
    {
        {
            builder.ToTable("Citas");
            builder.Property(p => p.Fecha)
            .HasColumnType("Date")
            .IsRequired();

            builder.Property(p => p.Hora)
            .HasColumnType("Time")
            .IsRequired();

            builder.Property(p => p.Motivo)
            .IsRequired();
            
            builder.HasOne(p => p.Veterinario)
            .WithMany(f => f.Citas)
            .HasForeignKey(fk => fk.IdVeterinario)
            .IsRequired();
            
            builder.HasOne(p => p.Mascota)
            .WithMany(f => f.Citas)
            .HasForeignKey(fk => fk.IdMascota)
            .IsRequired();
        }
    }
}