using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.CalificacionRollosEnProceso
{
    public class EPartidaCab
    {
        public string usuario { get; set; } = string.Empty;
        public string auditor { get; set; } = string.Empty;
        public string supervisor { get; set; } = string.Empty;
        public string maquina { get; set; } = string.Empty;
        public string turno { get; set; } = string.Empty;
        public string unidadNegocio { get; set; } = string.Empty;
        public string estadoPartida { get; set; } = string.Empty;
        public string procesoAuditado { get; set; } = string.Empty;
        public string calificacion { get; set; } = string.Empty;
        public string observaciones { get; set; } = string.Empty;
        public string datosPartida { get; set; } = string.Empty;
        public string estadoProceso { get; set; } = string.Empty;
        public string datosTela { get; set; } = string.Empty;
        public string datosCliente { get; set; } = string.Empty;
        public string ArchivoNombre { get; set; } = string.Empty;
        public string ancho { get; set; } = string.Empty;
        public string alto { get; set; } = string.Empty;
        public string largo { get; set; } = string.Empty;
        public string densidad { get; set; } = string.Empty;
        public string reproceso { get; set; } = string.Empty;

        public List<object> detPartida { get; set; }
        public List<object> detDefecto { get; set; }
        public List<object> detRectilineo { get; set; }
        public List<object>? detRollosTotal { get; set; } //Se agrega para que retenga todos los rollos.

    }
}

