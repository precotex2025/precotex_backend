using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.SolicitudMantenimiento
{
    public class TM_Notificacion_Incidencia
    {
        public DateTime Fecha { get; set; }
        public string? Maquina { get; set; }
        public string? Area { get; set; }
        public string? Turno { get; set; }
        public int Numero_Incidencia { get; set; }
        public int Horas_Paro_Maquina { get; set; }
        /*
         
FECHA	MAQUINA	AREA	TURNO	NUMERO_INCIDENCIA	HORAS_PARO_MAQUINA
2026-07-16	ABRIDO-01 	007	NOCHE	1	0        
          
         
         */
    }
}
