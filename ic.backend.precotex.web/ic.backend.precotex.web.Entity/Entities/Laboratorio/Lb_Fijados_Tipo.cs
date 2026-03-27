using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Laboratorio
{
    public class Lb_Fijados_Tipo
    {
        public int? Fij_Tip_Id          { get; set; }  
        public string? Fij_Tip_Des         { get; set; }
        public string? Flg_Est             { get; set; }
        public string? Usr_Reg             { get; set; }
        public DateTime? Fec_Reg             { get; set; }
        public string? Usr_Mod             { get; set; }
        public DateTime? Fec_Mod             { get; set; }
    }
}
