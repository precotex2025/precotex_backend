using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Laboratorio
{
    public class Lb_Jabonados
    {
        public int? Jab_Id { get; set; }
        public string? Jab_Fam { get; set; }
        public string? Jab_Des { get; set; }
        public decimal? Jab_Ran_Ini { get; set; }
        public decimal? Jab_Ran_Fin { get; set; }
        public int? Jab_Can { get; set; }        
    }
}
