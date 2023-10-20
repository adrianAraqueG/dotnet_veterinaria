using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
    public class VeterinarioRepository: GenericRepository<Veterinario>, IVeterinarioRepository
{
    private readonly APIContext _context;

    public VeterinarioRepository(APIContext context) : base(context)
    {
       _context = context;
    }

    public async Task<IEnumerable<Veterinario>> ObtenerVeterinariosXEspecialidad(string especialidad){
        IEnumerable<Veterinario> VetCV = await _context.Veterinarios
                                            .Where(p => p.Especialidad.ToLower() == especialidad.ToLower())
                                            .ToListAsync();
        return VetCV;
    }
}