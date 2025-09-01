using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities
{
    public class Tx_Ubicacion_Colgador_Items
    {
        public int? Id_Tx_Ubicacion_Colgador_Items { get; set; }
        public int? Id_Tx_Ubicacion_Colgador { get; set; }
        public int? Id_Tx_Colgador_Registro_Cab { get; set; }
        public string? Flg_Estatus { get; set; }
        public DateTime Fec_Registro { get; set; }
        public string? Usu_Registro { get; set; }
        public DateTime Fec_Modifica { get; set; }
        public string? Usu_Modifica { get; set; }
        public string? Cod_Equipo { get; set; }

        //otros 
        public string? Accion { get; set; } = null!;
        public string? CodigoBarra { get; set; } = null!;
        public int? Id_Tx_Ubicacion_Fisica { get; set; }

    }
}
