using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Laboratorio
{
    public class Lb_Tipo_tenido
    {
        public int? Tip_Ten_Id { get; set; }
        public string? Tip_Ten_Des { get; set; }
        public string? Tip_Ten_Acr { get; set; }
        public string? Flg_Status { get; set; }
        public string? Usr_Reg { get; set; }
        public DateTime Fec_Reg { get; set; }
        public string? Usr_Mod { get; set; }
        public DateTime Fec_Mod { get; set; }

    }
}