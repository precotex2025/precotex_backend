using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.CalificacionRollosEnProceso
{
    public class EDefectos
    {
        public string DEFECTO { get; set; } = string.Empty;
        public string COD_MOTIVO { get; set; } = string.Empty;
        public string FLG_ORDEN { get; set; } = string.Empty;
        public int PAGINA { get; set; }
    }

    public class EDefectosRegistrados
    {
        public string? Cod_Tela { get; set; }
        public string? Codigo_Rollo { get; set; }
        public string? Descripcion { get; set; }
        public int? MTRS { get; set; }
        public int? Grado { get; set; }
        public decimal? Cantidad { get; set; }
        public string? Cod_Motivo { get; set; }
    }
}
