using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.CalificacionRollosEnProceso
{
    public  class EUnionRollos
    {
        public int Id { get; set; }

        public string Cod_Ordtra { get; set; } = string.Empty;
        public string Rollo { get; set; } = string.Empty;
        public string Kgs_crudo { get; set; } = string.Empty;
        public string Tela_comb { get; set; } = string.Empty;
        public string Und_crudo { get; set; } = string.Empty;
        public string Med_std { get; set; } = string.Empty;
        public string Opcion { get; set; } = string.Empty;
        public int Calidad { get; set; }
    }
}
