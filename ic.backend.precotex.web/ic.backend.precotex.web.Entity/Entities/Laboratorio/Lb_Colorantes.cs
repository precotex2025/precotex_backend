using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Laboratorio
{
    public class Lb_Colorantes
    {
        public int? Col_Id { get; set; }
        public string? Col_Cod_AutoLab { get; set; }
        public string? Col_Des { get; set; }
        public string? Col_Cod_Org { get; set; }
        public decimal? Col_Ini { get; set; }
        public int? Col_Flg_Status { get; set; }

    }
}
