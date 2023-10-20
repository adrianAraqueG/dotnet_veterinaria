namespace API.Dtos
{
    public class MascotaSimpleDto
    {
        public int Id {get; set;}
        public string Nombre {get; set;}
        public DateTime FechaNacimiento {get; set;}
        public int IdPropietario {get; set;}
        public int IdEspecie {get; set;}
        public int IdRaza {get; set;}

    }
}