using Domain.Entities;
using Domain.Interfaces;
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
}