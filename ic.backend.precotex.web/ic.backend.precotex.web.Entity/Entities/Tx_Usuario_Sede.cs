using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities
{
    public class Tx_Usuario_Sede
    {
        public string? Cod_Usuario { get; set; }
        public int? Num_Planta { get; set; }
        public string? Flg_Estado { get; set; }
        public string? Des_Planta { get; set; }
    }
}
