using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Laboratorio
{
    public class Lb_ColTra_Cab
    {
        public string? Cod_Cliente { get; set; }
        public string? Des_Cliente { get; set; }
        public int? Corr_Carta { get; set; }
        public string? Cod_Tela { get; set; }
        public string? Des_Tela { get; set; }
        public DateTime? Fec_creacion { get; set; }
        public string? Lab_Dias { get; set; }
        public DateTime? Fec_compromiso { get; set; }
        public int? Dias_Falt_Compromiso { get; set; }
        public string? Estado { get; set; }
        public string? Flg_Est_Lab { get; set; }
        public string? Cod_Ruta { get; set; }
    }
}
