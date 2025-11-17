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
    }
}
