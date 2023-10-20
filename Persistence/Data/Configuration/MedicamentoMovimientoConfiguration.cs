using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistence.Data.Configuration;
public class MedicamentoMovimientoConfiguration : IEntityTypeConfiguration<MedicamentoMovimiento>
{
    public void Configure(EntityTypeBuilder<MedicamentoMovimiento> builder)
    {
        {
            builder.ToTable("MedicamentoMovimientos");
            builder.Property(p => p.Cantidad)
            .IsRequired();
            builder.Property(p => p.Fecha)
            .IsRequired();
            builder.Property(p => p.PrecioUnitario)
            .IsRequired();
            builder.HasOne(p => p.Medicamento)
            .WithMany(f => f.MedicamentosMovimientos)
            .HasForeignKey(fk => fk.IdMedicamento)
            .IsRequired();
            builder.HasOne(p => p.TipoMovimiento)
            .WithMany(f => f.MedicamentoMovimientos)
            .HasForeignKey(fk => fk.IdTipoMovimiento)
            .IsRequired();
        }
    }
}