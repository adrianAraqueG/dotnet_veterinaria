using Domain.Entities;

namespace API.Dtos;

public class CitaDto{
        public int Id {get; set;}
        public DateTime Fecha {get; set;}
        public TimeSpan Hora {get; set;}
        public string Motivo {get; set;}
        public int IdMascota {get; set;}
        public MascotaSimpleDto Mascota {get; set;}
        public int IdVeterinario {get; set;}
        public VeterinarioDto Veterinario {get; set;}
}