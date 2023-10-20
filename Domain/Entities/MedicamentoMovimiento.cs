using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MedicamentoMovimiento : BaseEntity
    {
        public int Cantidad {get; set;}
        public DateTime Fecha {get; set;}
        public double PrecioUnitario {get; set;}
        public int IdMedicamento {get; set;}
        public Medicamento Medicamento {get; set;}
        public int IdTipoMovimiento {get; set;}
        public TipoMovimiento TipoMovimiento {get; set;}
    }
}