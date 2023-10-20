using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
    public class MascotaRepository: GenericRepository<Mascota>, IMascotaRepository
{
    private readonly APIContext _context;

    public MascotaRepository(APIContext context) : base(context)
    {
       _context = context;
    }

    public async Task<IEnumerable<Mascota>> ObtenerPorEspecie(string especie){
        IEnumerable<Mascota> result = await _context.Mascotas
                                                    .Include(m => m.Raza)
                                                    .ThenInclude(r => r.Especie) 
                                                    .Where(m => m.Raza.Especie.Nombre == especie)
                                                    .ToListAsync();
        return result;
    }
    public async Task<IEnumerable<Mascota>> ObtenerMascXVeterinario(int IdVet){
        IEnumerable<Mascota> result = await _context.Citas
                                    .Where(c => c.IdVeterinario == IdVet)
                                    .Select(c => c.Mascota)
                                    .ToListAsync();
        return result;
    }

    public async Task<object> ObtenerAgrupadasPorEspecie(){
        var result = await (from m in _context.Mascotas
                       join r in _context.Razas on m.IdRaza equals r.Id
                       join e in _context.Especies on r.IdEspecie equals e.Id
                       group m by e.Nombre into grouped
                       select new 
                       {
                           Especie = grouped.Key,
                           Mascotas = grouped.Select(m => m.Nombre).ToList()
                       }).ToListAsync();

        return result;
    }
}