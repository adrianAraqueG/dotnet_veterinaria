using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistence.Data.Configuration;
public class TratamientoConfiguration : IEntityTypeConfiguration<Tratamiento>
{
    public void Configure(EntityTypeBuilder<Tratamiento> builder)
    {
        {
            builder.ToTable("Tratamientos");
            builder.Property(p => p.Dosis)
            .HasMaxLength(100)
            .IsRequired();
            builder.Property(p => p.Administracion)
            .IsRequired();
            builder.Property(p => p.Observaciones)
            .HasMaxLength(100)
            .IsRequired();
            builder.HasOne(p => p.Cita)
            .WithMany(f => f.Tratamientos)
            .HasForeignKey(fk => fk.IdCita)
            .IsRequired();
            builder.HasOne(p => p.Medicamento)
            .WithMany(f => f.Tratamientos)
            .HasForeignKey(fk => fk.IdMedicamento)
            .IsRequired();
        }
    }
}