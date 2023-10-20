using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
    public class PropietarioRepository: GenericRepository<Propietario>, IPropietarioRepository
{
    private readonly APIContext _context;

    public PropietarioRepository(APIContext context) : base(context)
    {
       _context = context;
    }

    public async Task<IEnumerable<object>> ObtenerPropsConMasc(){
        var result = _context.Propietarios
                                     .Select(p => new 
                                     {
                                         PropietarioId = p.Id,
                                         NombrePropietario = p.Nombre,
                                         Email = p.Email,
                                         Telefono = p.Telefono,
                                         Mascotas = p.Mascotas.Select(m => new 
                                         {
                                             MascotaId = m.Id,
                                             NombreMascota = m.Nombre,
                                             FechaNacimiento = m.FechaNacimiento,
                                             Especie = m.Especie.Nombre,
                                             Raza = m.Raza.Nombre
                                         }).ToList()
                                     }).ToList();
        return result;
    }
}