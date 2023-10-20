namespace Domain.Entities;

public class TipoMovimiento : BaseEntity
{
    public string Nombre {get; set;}
    public ICollection<MedicamentoMovimiento> MedicamentoMovimientos {get; set;}
}