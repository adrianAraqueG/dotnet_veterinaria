using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
    public class MedicamentoRepository: GenericRepository<Medicamento>, IMedicamentoRepository
{
    private readonly APIContext _context;

    public MedicamentoRepository(APIContext context) : base(context)
    {
       _context = context;
    }

    public async Task<IEnumerable<Medicamento>> ObtenerPrecioMayorAAsync(int cantidad){
        IEnumerable<Medicamento> result = await _context.Medicamentos
                                    .Where(m => m.Precio >= cantidad)
                                    .ToListAsync();
        return result;
    }

    public async Task<IEnumerable<Medicamento>> ObtenerMedsXLabAsync(string laboratorio){
        IEnumerable<Medicamento> result = await _context.Medicamentos
                                                .Include(m => m.Laboratorio)
                                                .Where(m => m.Laboratorio.Nombre == laboratorio)
                                                .ToListAsync();
        return result;
    }
}