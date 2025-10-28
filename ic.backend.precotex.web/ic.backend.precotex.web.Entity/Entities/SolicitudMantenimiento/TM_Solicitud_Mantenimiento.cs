using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.SolicitudMantenimiento
{
    public class TM_Solicitud_Mantenimiento
    {
        public string? Cod_Solicitud { get; set; }
        public string? Cod_Estado_Mant { get; set; }
        public string? Cod_Area { get; set; }
        public string? Cod_Maquina { get; set; }
        public string? Observacion { get; set; }
        public string? Prioridad { get; set; }
        public bool? Paro_Maquina { get; set; }
        public string? Ruta_Fotografia { get; set; }
        public string? Hora_Inicio { get; set; }
        public string? Hora_Fin { get; set; }
        public string? Fec_Registro { get; set; }
        public string? Usu_Registro { get; set; }
        public string? Cod_Equipo { get; set; }
        public int? T1_Tiempo_Espera_Min { get; set; }
        public int? T2_Tiempo_Interv_Min { get; set; }
        public int? T3_Tiempo_VB_Min { get; set; }
        public string? Cod_Usuario_Supervisor { get; set; }

        //otros campos
        public string? Area { get; set; }
        public string? Maquina { get; set; }
        public string? Paro_Maquina_Descripcion { get; set; }
        public string? Supervisor { get; set; }
        public string? Estado { get; set; }
        public int? Num_Planta { get; set; }


        /*
          
						Cod_Solicitud,
						Cod_Estado_Mant,
						Cod_Usuario_Supervisor,
						Cod_Area,
						Cod_Maquina,
						Observacion,
						Prioridad,
						Paro_Maquina,
						Ruta_Fotografia,
						Hora_Inicio,
						Hora_Fin,
						Fec_Registro,
						Usu_Registro,
						Cod_Equipo         
        
         
         */
    }
}
