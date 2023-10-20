using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
    public class MascotaRepository: GenericRepository<Mascota>, IMascotaRepository
{
    private readonly APIContext _context;

    public MascotaRepository(APIContext context) : base(context)
    {
       _context = context;
    }
}