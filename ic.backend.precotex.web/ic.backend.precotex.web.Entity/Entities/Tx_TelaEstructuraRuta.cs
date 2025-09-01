using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities
{
    public class Tx_TelaEstructuraRuta
    {
        public string? Cod_Ruta { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? Fec_Creacion { get; set; }
        public int IdUnico { get; set; }

    }
}
