using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        {
            builder.ToTable("Usuarios");
            builder.Property(p => p.Username)
            .HasColumnType("varchar(255) COLLATE utf8mb4_unicode_ci")
            .HasMaxLength(50)
            .IsRequired();
            builder.Property(p => p.Password)
           .HasColumnName("password")
           .HasColumnType("varchar(255) COLLATE utf8mb4_unicode_ci")
           .HasMaxLength(255)
           .IsRequired();
            builder.Property(p => p.Email)
            .HasColumnName("email")
            .HasColumnType("varchar(255) COLLATE utf8mb4_unicode_ci")
            .HasMaxLength(100)
            .IsRequired();
            builder.Property(p => p.DNI)
            .IsRequired()
            .HasMaxLength(15);
            builder.HasIndex(p => p.DNI)
            .IsUnique();
            builder
           .HasMany(p => p.Roles)
           .WithMany(r => r.Usuarios)
           .UsingEntity<UsuarioRol>(
               j => j
               .HasOne(pt => pt.Rol)
               .WithMany(t => t.UsuariosRoles)
               .HasForeignKey(ut => ut.IdRol),
               j => j
               .HasOne(et => et.Usuario)
               .WithMany(et => et.UsuarioRoles)
               .HasForeignKey(el => el.IdUsuario),
               j =>
               {
                   j.ToTable("UsuarioRol");
                   j.HasKey(t => new { t.IdUsuario, t.IdRol });

               });
            builder.HasMany(p => p.RefreshTokens)
            .WithOne(p => p.Usuario)
            .HasForeignKey(p => p.IdUsuario);
        }

    }
}
