using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
    public class LaboratorioRepository: GenericRepository<Laboratorio>, ILaboratorioRepository
{
    private readonly APIContext _context;

    public LaboratorioRepository(APIContext context) : base(context)
    {
       _context = context;
    }
}