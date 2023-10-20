using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
    public class ProveedorRepository: GenericRepository<Proveedor>, IProveedorRepository
{
    private readonly APIContext _context;

    public ProveedorRepository(APIContext context) : base(context)
    {
       _context = context;
    }
}