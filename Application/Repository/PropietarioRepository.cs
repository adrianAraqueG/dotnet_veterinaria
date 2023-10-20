using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
    public class PropietarioRepository: GenericRepository<Propietario>, IPropietarioRepository
{
    private readonly APIContext _context;

    public PropietarioRepository(APIContext context) : base(context)
    {
       _context = context;
    }
}