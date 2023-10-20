namespace Domain.Entities
{
    public class Tratamiento : BaseEntity
    {
        public string Dosis {get; set;}
        public DateTime Administracion {get; set;}
        public string Observaciones {get; set;}
        public int IdCita {get; set;}
        public Cita Cita {get; set;}
        public int IdMedicamento {get; set;}
        public Medicamento Medicamento {get; set;}
    }
}