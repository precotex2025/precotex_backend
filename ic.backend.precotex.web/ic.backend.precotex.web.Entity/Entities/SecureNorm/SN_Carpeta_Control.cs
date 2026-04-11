using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.SecureNorm
{
    public class SN_Carpeta_Control
    {
        public string CodigoCarpetaControl { get; set; } = null!;
        public string? CodigoSede { get; set; } 
        public string Denominacion { get; set; } = null!;
        public string TipoCarpeta { get; set; } = null!; // CHAR(1), "1"=CONTROLADO, "2"=NO CONTROLADO
        public bool FlgActivo { get; set; } = true; 
        public string CodUsuario { get; set; } = null!; 
        public DateTime FecRegistro { get; set; } = DateTime.Now; 
        public string? CodUsuarioModifico { get; set; } 
        public DateTime? FecModificacion { get; set; }
    }
}
