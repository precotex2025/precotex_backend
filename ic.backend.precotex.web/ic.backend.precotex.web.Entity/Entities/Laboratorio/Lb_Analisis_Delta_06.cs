using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Laboratorio
{
    public class Lb_Analisis_Delta_06
    {
        public int SampleId { get; set; }
        public string? Name { get; set; }
        public int MeasId { get; set; }
        public decimal Est_CIE_L { get; set; }
        public decimal Est_CIE_a { get; set; }
        public decimal Est_CIE_b { get; set; }
        public decimal Est_CIE_C { get; set; }
        public decimal Est_CIE_h { get; set; }
        public string? Especularidad { get; set; }
        public string? Filtro_UV { get; set; }

    }
}
