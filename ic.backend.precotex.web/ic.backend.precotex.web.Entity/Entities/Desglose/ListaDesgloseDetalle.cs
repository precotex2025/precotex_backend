using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Desglose
{
    public class ListaDesgloseDetalle
    {

        public int Id_Desglose { get; set; }
        public string Auditor { get; set; }
        public string Cod_Ordtra { get; set; }
        public string Cod_Color { get; set; }
        public string Proveedor { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal Total { get; set; }
        public decimal Punio { get; set; }
        public decimal Cuello { get; set; }
        public decimal Pechera { get; set; }
        public decimal Tira { get; set; }

        public decimal Pretina { get; set; }
        public decimal Colitas { get; set; }
    }
}
