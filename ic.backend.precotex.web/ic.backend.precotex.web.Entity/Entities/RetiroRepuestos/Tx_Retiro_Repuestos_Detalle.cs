using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.RetiroRepuestos
{
    public class Tx_Retiro_Repuestos_Detalle
    {
        public int Num_Requerimiento { get; set; }
        public int? Nro_Secuencia { get; set; }
        public string? Itm_Codigo { get; set; }
        public string? Itm_Descripcion { get; set; }
        public double? Itm_Cantidad { get; set; }
        public string? Itm_Unidad_Medida { get; set; }
        public string? Rpt_Cambio { get; set; }
        public string? Itm_Foto { get; set; }
    }
}
