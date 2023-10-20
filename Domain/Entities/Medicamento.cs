namespace Domain.Entities
{
    public class Medicamento : BaseEntity
    {
        public string Nombre {get; set;}
        public int Stock {get; set;}
        public double Precio { get; set; }
        public int IdLaboratorio {get; set;}
        public Laboratorio Laboratorio {get; set;}
        public int IdProveedor {get; set;}
        public Proveedor Proveedor {get; set;}
        public ICollection<Tratamiento> Tratamientos {get; set;}
        public ICollection<MedicamentoMovimiento> MedicamentosMovimientos {get; set;}

    }
}