using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.QR
{
    public class E_RegistroQR
    {
        public string CodMotivo { get; set; }
        public string CodMaquina { get; set; }
        public string CodUsuario { get; set; } // ¡Este es el que te faltaba enviar!
        public string Observacion { get; set; }
    }
}
