using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Cotizaciones
{
    public class Tx_PreciosColor
    {
        public string? CORR_CARTA { get; set; }
        public int TIEMPO { get; set; }
        public decimal PREC_TINTO { get; set; }
        public decimal PREC_ACABADO { get; set; }
    }
}
