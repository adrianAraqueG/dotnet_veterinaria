using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
    public class TratamientoRepository: GenericRepository<Tratamiento>, ITratamientoRepository
{
    private readonly APIContext _context;

    public TratamientoRepository(APIContext context) : base(context)
    {
       _context = context;
    }
}