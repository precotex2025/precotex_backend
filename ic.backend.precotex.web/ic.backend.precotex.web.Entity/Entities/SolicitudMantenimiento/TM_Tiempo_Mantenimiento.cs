using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.SolicitudMantenimiento
{
    public class TM_Tiempo_Mantenimiento
    {
        public string? Accion { get; set; }
        public int? Num_Mante { get; set; }
        //public DateTime? Fec_Registro { get; set; }
        public string? Cod_Maquina { get; set; }
        public string? Nro_DocIde { get; set; }
        public string? Cod_Tarea { get; set; }
        public string? Cod_Ordtra { get; set; }
        //public string? Fec_Hora_Inicio { get; set; }
        //public string? Fec_Hora_Fin { get; set; }
        public string? ObserMante { get; set; }
        public string? Cod_Usuario { get; set; }
        public string? Cod_Espe { get; set; }
        public string? Cod_Articulo { get; set; }
        public string? Cod_Area_Tej_Mante_Maq { get; set; }
        public string? Cod_Tej_Cond { get; set; }
        public string? Cod_ParMaq_Tej { get; set; }
        public string? Cod_TipFall { get; set; }
        public string? ObserMante2 { get; set; }
        public string? Flg_Atribuido { get; set; }
        public int?  Num_Planta { get; set; }
        public string? Cod_Solicitud { get; set; }
        public string? Datos_Lider { get; set; }

        // Resultado del SP
        //public string? Respuesta { get; set; }
        //public string? Nombre_Tarea { get; set; }
        //public string? Trabajador { get; set; }
        //public string? Fec_Hora_Inicio_Str { get; set; }
        //public string? Fec_Hora_Fin_Str { get; set; }
    }
}
