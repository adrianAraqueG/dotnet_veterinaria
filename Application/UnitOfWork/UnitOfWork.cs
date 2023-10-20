using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Repository;
using Domain.Interfaces;
using Persistence;

namespace Application.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly APIContext _context;

    private IRolRepository _roles;
    private IUsuarioRepository _usuarios;
    private IUsuarioRolRepository _usuarioRoles;
    private ILaboratorioRepository _laboratorios;
    private IMedicamentoRepository _medicamentos;
    private IMedicamentoMovimientoRepository _medicamentoMovimientos;
    private IPropietarioRepository _propietarios;
    private IMascotaRepository _mascotas;
    private IProveedorRepository _proveedores;
    private IRazaRepository _razas;
    private IEspecieRepository _especies;
    private ITratamientoRepository _tratamientos;
    private ITipoMovimientoRepository _tipoMovimientos;
    private IVeterinarioRepository _veterinarios;
    private ICitaRepository _citasRepository;
    
    public UnitOfWork(APIContext context)
    {
        _context = context;
    }
    public IRolRepository Roles
    {
        get
        {
            if (_roles == null)
            {
                _roles = new RolRepository(_context);
            }
            return _roles;
        }
    }
    public IUsuarioRolRepository UsuarioRoles
    {
        get
        {
            if (_usuarioRoles == null)
            {
                _usuarioRoles = new UsuarioRolRepository(_context);
            }
            return _usuarioRoles;
        }
    }

    public IUsuarioRepository Usuarios
    {
        get
        {
            if (_usuarios == null)
            {
                _usuarios = new UsuarioRepository(_context);
            }
            return _usuarios;
        }
    }

    public ICitaRepository Citas
    {
        get
        {
            if (_citasRepository == null)
            {
                _citasRepository = new CitaRepository(_context);
            }
            return _citasRepository;
        }
    }
    public ILaboratorioRepository Laboratorios
    {
        get
        {
            if (_laboratorios == null)
            {
                _laboratorios = new LaboratorioRepository(_context);
            }
            return _laboratorios;
        }
    }

    public IMedicamentoRepository Medicamentos
    {
        get
        {
            if (_medicamentos == null)
            {
                _medicamentos = new MedicamentoRepository(_context);
            }
            return _medicamentos;
        }
    }

    public IMedicamentoMovimientoRepository MedicamentoMovimientos
    {
        get
        {
            if (_medicamentoMovimientos == null)
            {
                _medicamentoMovimientos = new MedicamentoMovimientoRepository(_context);
            }
            return _medicamentoMovimientos;
        }
    }

    public IPropietarioRepository Propietarios 
    {
        get
        {
            if (_propietarios == null)
            {
                _propietarios = new PropietarioRepository(_context);
            }
            return _propietarios;
        }
    }

    public IRazaRepository Razas
    {
        get
        {
            if (_razas == null)
            {
                _razas = new RazaRepository(_context);
            }
            return _razas;
        }
    }

    public IEspecieRepository Especies
    {
        get
        {
            if (_especies == null)
            {
                _especies = new EspecieRepository(_context);
            }
            return _especies;
        }
    }

    public ITratamientoRepository Tratamientos
    {
        get
        {
            if (_tratamientos == null)
            {
                _tratamientos = new TratamientoRepository(_context);
            }
            return _tratamientos;
        }
    }

    public ITipoMovimientoRepository TipoMovimientos
    {
        get
        {
            if (_tipoMovimientos == null)
            {
                _tipoMovimientos = new TipoMovimientoRepository(_context);
            }
            return _tipoMovimientos;
        }
    }

    public IVeterinarioRepository Veterinarios
    {
        get
        {
            if (_veterinarios == null)
            {
                _veterinarios = new VeterinarioRepository(_context);
            }
            return _veterinarios;
        }
    }

    public IMascotaRepository Mascotas
    {
        get
        {
            if (_mascotas == null)
            {
                _mascotas = new MascotaRepository(_context);
            }
            return _mascotas;
        }
    }

    public IProveedorRepository Proveedores
    {
        get
        {
            if (_proveedores == null)
            {
                _proveedores = new ProveedorRepository(_context);
            }
            return _proveedores;
        }
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    } 
}
