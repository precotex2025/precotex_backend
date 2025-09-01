using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities
{
    public class E_RegistroPartidaParihuela
    {
        public string? CodigoPartida { get; set; }
        public string? CodigoParihuela { get; set; }
        public decimal? PesoParihuela { get; set; }
        public decimal? PesoBruto { get; set; }
        public decimal? PesoNeto { get; set; }
        public string? Complemento { get; set; }
        public decimal? PesoComplemento { get; set; }
    }
}
