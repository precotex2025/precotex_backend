using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.QuejasReclamos
{
    public  class FiltroReclamoDto
    {
        public string? NroCaso { get; set; }
        public string? Cliente { get; set; }
        public string? TipoRegistro { get; set; }
        public string? EstadoSolicitud { get; set; }
        public string? Responsable { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

        public DateTime? FechaRegistro { get; set; }
        public string? Observacion { get; set; }
        public string? UsuarioRegistro { get; set; }
    }
}
