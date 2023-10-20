using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Configuration;

namespace Persistence;

public class APIContext : DbContext
{
    public APIContext(DbContextOptions<APIContext> options) : base(options){}

    public DbSet<Cita> Citas {get; set;}
    public DbSet<Especie> Especies {get; set;}
    public DbSet<Laboratorio> Laboratorios {get; set;}
    public DbSet<Mascota> Mascotas {get; set;}
    public DbSet<Medicamento> Medicamentos {get; set;}
    public DbSet<MedicamentoMovimiento> MedicamentoMovimientos {get; set;}
    public DbSet<Propietario> Propietarios {get; set;}
    public DbSet<Proveedor> Proveedores {get; set;}
    public DbSet<Raza> Razas {get; set;}
    public DbSet<Rol> Roles {get; set;}
    public DbSet<TipoMovimiento> TipoMovimientos {get; set;}
    public DbSet<Tratamiento> Tratamientos {get; set;}
    public DbSet<Usuario> Usuarios {get; set;}
    public DbSet<UsuarioRol> UsuarioRoles {get; set;}
    public DbSet<Veterinario> Veterinarios {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CitaConfiguration());
        modelBuilder.ApplyConfiguration(new EspecieConfiguration());
        modelBuilder.ApplyConfiguration(new LaboratorioConfiguration());
        modelBuilder.ApplyConfiguration(new MascotaConfiguration());
        modelBuilder.ApplyConfiguration(new MedicamentoConfiguration());
        modelBuilder.ApplyConfiguration(new MedicamentoMovimientoConfiguration());
        modelBuilder.ApplyConfiguration(new PropietarioConfiguration());
        modelBuilder.ApplyConfiguration(new ProveedorConfiguration());
        modelBuilder.ApplyConfiguration(new RazaConfiguration());
        modelBuilder.ApplyConfiguration(new RolConfiguration());
        modelBuilder.ApplyConfiguration(new TipoMovimientoConfiguration());
        modelBuilder.ApplyConfiguration(new TratamientoConfiguration());
        modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
        modelBuilder.ApplyConfiguration(new VeterinarioConfiguration());
        
    }
}
