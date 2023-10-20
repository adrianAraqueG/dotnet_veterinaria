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
}