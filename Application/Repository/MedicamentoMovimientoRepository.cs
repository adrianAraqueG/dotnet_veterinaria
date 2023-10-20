using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
    public class MedicamentoMovimientoRepository: GenericRepository<MedicamentoMovimiento>, IMedicamentoMovimientoRepository
{
    private readonly APIContext _context;

    public MedicamentoMovimientoRepository(APIContext context) : base(context)
    {
       _context = context;
    }
}