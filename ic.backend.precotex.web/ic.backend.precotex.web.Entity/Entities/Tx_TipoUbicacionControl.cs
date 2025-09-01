using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities
{
    public class Tx_TipoUbicacionControl
    {
        public int? Id_Tipo_Ubicacion_Control { get; set; }
        public int? Id_Tipo_Ubicacion_Colgador { get; set; }
        public string? Cod_FamTela { get; set; }
        public string? Cod_Tipo { get; set; }
        public int? Numero_Correlativo { get; set; }

        //Otros
        public string? QR_Correlativo { get; set; }

    }
}
