using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Laboratorio
{
    public class Lb_Analisis_Delta_01
    {
        public string? Cod_OrdTra { get; set; }
        public string? Cod_Muestra { get; set; }
        public string?  Des_Muestra { get; set; }
        public string? Cod_Standar { get; set; }
        public string? Des_Standar { get; set; }
        public int Can_Standar { get; set; }
        public string? Cod_Color { get; set; }
        public string?  Des_Color { get; set; }
        public string? Cod_Tela { get; set; }
        public string? Des_Tela { get; set; }

    }
}
