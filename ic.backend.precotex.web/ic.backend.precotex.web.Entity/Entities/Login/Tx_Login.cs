using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Login
{
    public class Tx_Login
    { 
        public string? Cod_Usuario { get; set; }
        public string? Password { get; set; }
        public string? Respuesta { get; set; }
        public string? Cod_Rol { get; set; }
        public string? Des_Rol { get; set; }
        public string? Cod_Empresa { get; set; }
        public string? Empresa { get; set; }
        public string? Tip_Trabajador { get; set; }
        public string? Cod_Trabajador { get; set; }
    }
}
