using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistence.Data.Configuration;
public class MedicamentoConfiguration : IEntityTypeConfiguration<Medicamento>
{
    public void Configure(EntityTypeBuilder<Medicamento> builder)
    {
        {
            builder.ToTable("Medicamentos");
            builder.Property(p => p.Nombre)
            .HasMaxLength(100)
            .IsRequired();

            builder.Property(p => p.Stock)
            .IsRequired();

            builder.HasOne(p => p.Laboratorio)
            .WithMany(f => f.Medicamentos)
            .HasForeignKey(fk => fk.IdLaboratorio)
            .IsRequired();
            
            builder.HasOne(p => p.Proveedor)
            .WithMany(f => f.Medicamentos)
            .HasForeignKey(fk => fk.IdProveedor)
            .IsRequired();
        }
    }
}