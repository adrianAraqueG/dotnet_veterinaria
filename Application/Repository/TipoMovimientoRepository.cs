using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
    public class TipoMovimientoRepository: GenericRepository<TipoMovimiento>, ITipoMovimientoRepository
{
    private readonly APIContext _context;

    public TipoMovimientoRepository(APIContext context) : base(context)
    {
       _context = context;
    }
}