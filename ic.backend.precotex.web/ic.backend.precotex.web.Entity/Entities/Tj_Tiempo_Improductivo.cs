using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities
{
    public class Tj_Tiempo_Improductivo
    {
        public DateTime? Fec_Registro { get; set; }
        public string? Cod_Maquina { get; set; }
        public string? Tip_Trabajador_Tejedor { get; set; }
        public string? Cod_Trabajador_Tejedor { get; set; }
        public string? Cod_Motivo { get; set; }
        public DateTime? Fec_Hora_Inicio { get; set; }
        public DateTime? Fec_Hora_Fin { get; set; }
        public string? Observacion { get; set; }
        public DateTime? Fec_Creacion { get; set; }
        public string? Cod_Usuario { get; set; }
        public string? Cod_Equipo { get; set; }


        /*

	datetime
	char
	Tip_Trabajador
	Cod_Trabajador
	char
	datetime
	datetime
	varchar
	datetime
	Cod_Usuario
	Cod_Equipo         
         */
    }
}
