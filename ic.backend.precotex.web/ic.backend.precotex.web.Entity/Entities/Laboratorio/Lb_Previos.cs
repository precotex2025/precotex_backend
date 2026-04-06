using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Laboratorio
{
    public class Lb_Previos
    {
        public int? Pre_Id { get; set; }
        public string? Pre_Des { get; set; }
        public string? Flg_Status { get; set; }
        public string? Usr_Reg { get; set; }
        public DateTime Fec_Reg { get; set; }
        public string? Usr_Mod { get; set; }
        public DateTime Fec_Mod { get; set; }

    }
}
