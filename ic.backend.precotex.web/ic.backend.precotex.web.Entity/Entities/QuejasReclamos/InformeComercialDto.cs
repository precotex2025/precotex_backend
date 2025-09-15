using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.QuejasReclamos
{
    public class InformeComercialDto
    {
        public string? Cod_Tipo_Consecuencia { get; set; }
        public string? Cod_SubTipo_Devolucion { get; set; }
        public string? Flg_NotaCredito { get; set; }
        public string? Observacion_Comercial_Cierre { get; set; }
    }
}
