using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistence.Data.Configuration;
public class RazaConfiguration : IEntityTypeConfiguration<Raza>
{
    public void Configure(EntityTypeBuilder<Raza> builder)
    {
        {
            builder.ToTable("Razas");
            builder.Property(p => p.Nombre)
            .HasMaxLength(100)
            .IsRequired();
            builder.HasIndex(p => p.Nombre)
            .IsUnique();
            builder.HasOne(p => p.Especie)
            .WithMany(f => f.Razas)
            .HasForeignKey(fk => fk.IdEspecie)
            .IsRequired();
        }
    }
}