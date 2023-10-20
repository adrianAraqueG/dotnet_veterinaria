using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
    public class EspecieRepository: GenericRepository<Especie>, IEspecieRepository
{
    private readonly APIContext _context;

    public EspecieRepository(APIContext context) : base(context)
    {
       _context = context;
    }
}