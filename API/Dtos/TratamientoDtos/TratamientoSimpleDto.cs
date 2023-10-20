namespace API.Dtos
{
    public class TratamientoSimpleDto
    {
        public int Id {get; set;}
        public string Dosis {get; set;}
        public DateTime Administracion {get; set;}
        public string Observaciones {get; set;}
        public int IdCita {get; set;}
        public int IdMedicamento {get; set;}
    }
}