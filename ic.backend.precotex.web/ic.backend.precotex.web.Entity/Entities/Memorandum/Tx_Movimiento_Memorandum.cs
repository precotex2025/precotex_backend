using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ic.backend.precotex.web.Entity.Entities.Memorandum
{
    public class Tx_Movimiento_Memorandum
    {

        public string? Num_Memo { get; set; }
        public DateTime? FechaMovimiento { get; set; }
        public string?  Cod_Usuario { get; set; }
        public string? Cod_Estado_Memo { get; set; }
        public string? Observaciones { get; set; }
        public string? Cod_Equipo { get; set; }

        //otros
        public string? NombreUsuario { get; set; }
        public string? EstadoDescripcion { get; set; }

    }
}
