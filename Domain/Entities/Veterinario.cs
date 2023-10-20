namespace Domain.Entities
{
    public class Veterinario : BaseEntity
    {
        public string Nombre {get; set;}
        public string Telefono {get; set;}
        public string Especialidad {get; set;}
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; } 
        public ICollection<Cita> Citas {get; set;}
        
    }
}