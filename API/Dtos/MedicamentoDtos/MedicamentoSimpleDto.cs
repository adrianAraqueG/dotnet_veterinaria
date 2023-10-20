namespace API.Dtos
{
    public class MedicamentoSimpleDto
    {
        public int Id {get; set;}
        public string Nombre {get; set;}
        public int Stock {get; set;}
        public double Precio { get; set; }
        public int IdLaboratorio {get; set;}
        public int IdProveedor {get; set;}

    }
}