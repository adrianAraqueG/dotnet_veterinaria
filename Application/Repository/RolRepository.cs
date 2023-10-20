using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class RolRepository : GenericRepository<Rol>, IRolRepository
{
    private readonly APIContext _context;

    public RolRepository(APIContext context) : base(context)
    {
       _context = context;
    }
}
