using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Laboratorio
{
    public class Lb_Fijados
    {
        public int? Fij_Id {get; set;}
        public string? Fij_Fam {get; set;}
        public string? Fij_Des {get; set;}
        public decimal? Fij_Ran_Ini {get; set;}
        public decimal? Fij_Ran_Fin { get; set; }
        public string? Usr_Reg { get; set; }
        public string? Usr_Mod { get; set; }
        public string? Fec_Reg { get; set; }
        public string? Fec_Mod { get; set; }
        public string? Flg_Status { get; set; }
    }

    public class Lb_Fijados_Detalle
    {
        public int? Fij_Id { get; set; }
        public decimal? Fij_Ran_Ini { get; set; }
        public decimal? Fij_Ran_Fin { get; set; }
        public string? Familia { get; set; }
        public string? Flg_Status { get; set; }
        public string? Fec_Reg { get; set; }
        public string? Usr_Reg { get; set; }
        public string? Fec_Mod { get; set; }
        public string? Usr_Mod { get; set; }
        public decimal? Fij_Ran_Ini_Org { get; set; }
        public string? Familia_Org { get; set; }
    }
}
