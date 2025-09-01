using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Desglose
{
    public class E_RegistroDesgloseRequest
    {

        public string Auditor { get; set; }
        public int Colitas { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Partida { get; set; }
        public string Proveedor { get; set; }
        public decimal Total { get; set; }
        public string UsuarioCrea { get; set; }

        public List<E_DesgloseItem> Items { get; set; }
    }
}
