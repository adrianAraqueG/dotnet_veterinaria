namespace API.Dtos
{
    public class MedicamentoDto
    {
        public int Id {get; set;}
        public string Nombre {get; set;}
        public int Stock {get; set;}
        public double Precio { get; set; }
        public LaboratorioSimpleDto Laboratorio {get; set;}
        public ProveedorSimpleDto Proveedor {get; set;}
        public ICollection<TratamientoSimpleDto> Tratamientos {get; set;}
        public ICollection<MedicamentoMovimientoSimpleDto> MedicamentosMovimientos {get; set;}

    }
}