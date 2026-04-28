using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Cotizaciones
{
    public class Tx_HilosTel
    {
        public decimal? Porcentaje { get; set; }
        public decimal? Precio_Final { get; set; }
        public decimal? Total { get; set; }
        public string? des_hiltel { get; set; }
        public string? Cod_Hilado_Estructurado { get; set; }
    }
}
