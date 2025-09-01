using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.CalificacionRollosEnProceso
{
    public class EPartidaPorRollo
    {
        public int Id { get; set; }
        public string CodPartida { get; set; } = string.Empty;
        public string CodRollo { get; set; } = string.Empty;
        public decimal Metros { get; set; }
        public string CodTela { get; set; } = string.Empty;
        public int Secuencia { get; set; }
        public int Calidad { get; set; }
        public string Cliente { get; set; } = string.Empty;
    }
}
