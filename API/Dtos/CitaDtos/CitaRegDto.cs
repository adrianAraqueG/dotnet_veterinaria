using Domain.Entities;

namespace API.Dtos;

public class CitaRegDto{
        public int? Id {get; set;}
        public DateTime Fecha {get; set;}
        public TimeSpan Hora {get; set;}
        public string Motivo {get; set;}
        public int IdMascota {get; set;}
        public int IdVeterinario {get; set;}
}