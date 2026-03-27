using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Cotizaciones
{
    public class Tx_Cotizaciones_Centro_Costo
    {
        public int? Cen_Cos_Cod             { get; set; }
        public string? Cen_Cos_Des          { get; set; }
        public string? Flg_Est              { get; set; }
        public string? Usr_Reg              { get; set; }
        public DateTime? Fec_Reg            { get; set; }
        public string? Usr_Mod              { get; set; }
        public DateTime? Fec_Mod            { get; set; }
    }
}
