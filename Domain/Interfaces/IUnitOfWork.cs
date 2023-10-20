using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces;

public interface IUnitOfWork
{
    IRolRepository Roles { get; }
    IUsuarioRepository Usuarios { get; }
    IUsuarioRolRepository UsuarioRoles {get; }
    ILaboratorioRepository Laboratorios {get; }
    IMedicamentoRepository Medicamentos {get; }
    IMedicamentoMovimientoRepository MedicamentoMovimientos {get; }
    IPropietarioRepository Propietarios {get; }
    IMascotaRepository Mascotas {get; }
    IProveedorRepository Proveedores {get; }
    IRazaRepository Razas {get; }
    IEspecieRepository Especies {get; }
    ITratamientoRepository Tratamientos {get; }
    ITipoMovimientoRepository TipoMovimientos {get; }
    IVeterinarioRepository Veterinarios {get; }
    ICitaRepository Citas {get; }
    Task<int> SaveAsync();

}