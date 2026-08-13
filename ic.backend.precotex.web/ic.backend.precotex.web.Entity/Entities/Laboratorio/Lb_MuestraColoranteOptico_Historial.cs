using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Laboratorio
{
    public class Lb_MuestraColoranteOptico_Historial
    {
        public string? Cod_Colorante_Optico { get; set; }
        public string? Des_Colorante_Optico { get; set; }
        public decimal? Can_Colorante_Optico { get; set; }
        public string? Usuario { get; set; }
        public DateTime? Fecha { get; set; }
    }
}
