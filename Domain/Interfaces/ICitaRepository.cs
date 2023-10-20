using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICitaRepository : IGenericRepository<Cita>
    {
        Task<string> RegisterAsync(Cita cita);

    }
}