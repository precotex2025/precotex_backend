using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities
{
    public class Tx_Ots_Hojas_Arranque_Ctrol
    {
        public string? Cod_OrdTra { get; set; }
        public int? Num_Secuencia { get; set; }
        public int? Version { get; set; }
        public DateTime? Fch_Hora_Entrega { get; set; }
        public string? Usu_Registro { get; set; }
    }
}
