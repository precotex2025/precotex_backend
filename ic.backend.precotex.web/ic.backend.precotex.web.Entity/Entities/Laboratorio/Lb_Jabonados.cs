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
        public string? Usr_Reg { get; set; }
        public string? Usr_Mod { get; set; }
        public string? Flg_Status { get; set; }
        public string? item { get; set; }
        public string? descripcion { get; set; }
        public string? usuario { get; set; }
    }

    public class Lb_Jabonados_Detalle
    {
        public int? Jab_Id { get; set; }   
        public decimal? Jab_Ran_Ini { get; set; }  
        public decimal? Jab_Ran_Fin { get; set; }  
        public int? Jab_Can { get; set; }  
        public string? Familia { get; set; } 
        public string? Usr_Reg { get; set; }  
        public string? Usr_Mod { get; set; }
        public string? Flg_Status { get; set; }
        public decimal? Jab_Ran_Ini_Org { get; set; }
        public string? Familia_Org { get; set; }
    }
    
}
