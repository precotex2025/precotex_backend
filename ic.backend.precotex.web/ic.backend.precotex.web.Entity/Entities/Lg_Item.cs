using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities
{
    public class Lg_Item
    {
        public string Cod_Item {  get; set; }
        public string Des_Item { get; set; }
        public string Cod_UniMed { get; set; }
        public double Can_Requerida { get; set; }
        public string Rpt_Cambio { get; set; }
        public string Itm_Foto { get; set; }
    }
}
