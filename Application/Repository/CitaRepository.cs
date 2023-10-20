using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
    public class CitaRepository: GenericRepository<Cita>, ICitaRepository
{
    private readonly APIContext _context;

    public CitaRepository(APIContext context) : base(context)
    {
       _context = context;
    }



    public override async Task<IEnumerable<Cita>> GetAllAsync()
    {
        return await _context.Citas
            .Include(p => p.Mascota)
            .Include(p => p.Veterinario)
            .ToListAsync();
    }



    public async Task<string> RegisterAsync(Cita model)
    {
        
        var existingPet = _context.Mascotas
            .Where(p => p.Id == model.IdMascota)
            .FirstOrDefault();

        if (existingPet == null)
        {
            return $"Lo sentimos, la mascota ingresada no existe.";
        }

        
        var existingVeterinario = _context.Veterinarios
            .Where(v => v.Id == model.IdVeterinario)
            .FirstOrDefault();

        if (existingVeterinario == null)
        {
            return $"Lo sentimos, el veterinario no existe.";
        }

        
        var Cita = new Cita
        {
            Fecha = model.Fecha,
            Hora = model.Hora,
            Motivo = model.Motivo,
            Mascota = existingPet,
            Veterinario = existingVeterinario,
        };

        try
        {
            _context.Citas.Add(Cita);
            await _context.SaveChangesAsync();

            return $"Cita guardada!";
        }
        catch (Exception ex)
        {
            var message = ex.Message;
            return $"No pudimos registrar la cita :( \n\n {message}";
        }
    }
}