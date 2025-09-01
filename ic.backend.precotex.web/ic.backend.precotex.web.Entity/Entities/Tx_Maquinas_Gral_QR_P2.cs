using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities
{
    public class Tx_Maquinas_Gral_QR_P2
    {
        public string? Cod_Maquina { get; set; }
        public string? Descripcion { get; set; }
        public string? Flg_Estatus { get; set; }
        public DateTime? Fec_Registro { get; set; }
        public string? Usu_Registro { get; set; }
        public string? Cod_Equipo { get; set; }
    }
}
