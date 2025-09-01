using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities
{
    public class Tx_Ubicacion_Colgador
    {
        public int? Id_Tx_Ubicacion_Colgador { get; set; }
        public int? Id_Tipo_Ubicacion_Colgador { get; set; }
        public int? Id_Tipo_Ubicacion_Colgador_Padre { get; set; }
        public string? CodigoBarra { get; set; }
        public string? Cod_FamTela { get; set; }
        public string? Flg_Estatus { get; set; }
        public DateTime Fec_Registro { get; set; }
        public string? Usu_Registro { get; set; }
        public DateTime Fec_Modifica { get; set; }
        public string? Usu_Modifica { get; set; }
        public string? Cod_Usuario { get; set; }
        public string? Cod_Equipo { get; set; }

        //OTROS
        public int? Correlativo { get; set; }
        public string? Descripcion { get; set; }
        public string? Des_FamTela { get; set; }
        public int? nTotalColgadores { get; set; }
        public int? nTotalCajas { get; set; }
    }
}
