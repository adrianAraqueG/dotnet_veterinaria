using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IMedicamentoRepository : IGenericRepository<Medicamento>
    {
        Task<IEnumerable<Medicamento>> ObtenerPrecioMayorAAsync(int cantidad);
        Task<IEnumerable<Medicamento>> ObtenerMedsXLabAsync(string laboratorio);
    }
}