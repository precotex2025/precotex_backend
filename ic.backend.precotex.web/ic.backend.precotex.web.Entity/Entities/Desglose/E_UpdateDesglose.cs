using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Desglose
{
    public class E_UpdateDesglose
    {

        public int Id_Desglose { get; set; }
        public string Auditor { get; set; }
        public string Cod_Color { get; set; }
        public string Cod_Ordtra { get; set; }

        public int Colitas { get; set; }

        public DateTime FechaFin { get; set; }
        public DateTime FechaInicio { get; set; }

        public string Proveedor { get; set; }
        public decimal Total { get; set; }

        public List<E_DesgloseItem> Items { get; set; }

        public E_UpdateDesglose()
        {
            Items = new List<E_DesgloseItem>();
        }

    }
}
