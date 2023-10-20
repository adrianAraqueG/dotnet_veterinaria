namespace API.Dtos
{
    public class MedicamentoMovimientoRegDto
    {
        public int Id {get; set;}
        public int Cantidad {get; set;}
        public DateTime Fecha {get; set;}
        public double PrecioUnitario {get; set;}
        public int IdMedicamento {get; set;}
        public int IdTipoMovimiento {get; set;}
    }
}