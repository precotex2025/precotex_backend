using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Laboratorio
{
    public class Lb_Estado_Dispensado
    {
        public string? Flg_Dispensando { get; set; }
        public string? Cod_Usuario_Dispensando { get; set; }
        public DateTime Fec_Dispensando { get; set; }
    }
}
