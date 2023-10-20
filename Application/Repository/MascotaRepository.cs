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
}