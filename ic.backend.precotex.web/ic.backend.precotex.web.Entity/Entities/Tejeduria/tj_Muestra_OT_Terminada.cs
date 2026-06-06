using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Tejeduria
{
    public class tj_Muestra_OT_Terminada
    {
        public string? Cod_Maquina { get; set; }
        public string? OT { get; set; }
        public string? Lote { get; set; }
        public string? Fibra { get; set; }
        public DateTime? Fec_Termino { get; set; }
        public string? Cod_Hilado { get; set; }
        public string? Articulo { get; set; }       
    }

    public class tj_Muestra_OT_Programada
    {
        public DateTime? Fec_Inicio { get; set; }
        public string? OT { get; set; }
        public string? Familia { get; set; }
        public string? Cod_Maquina { get; set; }
        public decimal? Can_Salida { get; set; }
        public decimal? Can_Teorico { get; set; }
        public decimal? Can_PorPedir { get; set; }
        public string? Lote { get; set; }
        public string? Cod_Hilado { get; set; } 
    }
}
