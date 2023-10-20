using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
    public class RazaRepository: GenericRepository<Raza>, IRazaRepository
{
    private readonly APIContext _context;

    public RazaRepository(APIContext context) : base(context)
    {
       _context = context;
    }
}