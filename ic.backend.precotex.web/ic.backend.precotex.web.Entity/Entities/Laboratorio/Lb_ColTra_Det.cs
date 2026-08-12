using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Laboratorio
{
    public class Lb_ColTra_Det
    {
        public string? Corr_Carta {  get; set; }
        public int Sec { get; set; }
        public int Correlativo { get; set; }
        public string? Descripcion_Color { get; set; }
        public string? Cod_Color {  get; set; }
        public string? Estandar_Tono_Comer {  get; set; }
        public int? Dias_Lab { get; set; }        
        public string? Formulado { get; set; }
        public decimal? Ph_Ini {get; set;}
        public decimal? Ph_Fin {get; set;}
        public decimal? Ph_Jab { get; set; }
        public decimal? Dosificacion1 { get; set; }
        public decimal? Dosificacion2 { get; set; }
        public decimal? Dosificacion3 { get; set; }
        public string? Flg_Est_Lab { get; set; }
        public decimal? Dosificacion1L { get; set; }
        public decimal? Dosificacion2L { get; set; }
        public decimal? Dosificacion3L { get; set; }
        public int? Cur_Ten { get; set; }
        public string? Cur_Des { get; set; }
        public string? Usr_Cod { get; set; }
        public decimal? Sod_Gr { get; set; }
        public int? Cur_Ten_Dis { get; set; }


        //OTROS CAMPOS 
        public string? Jab_Des { get; set; }
        public decimal? Volumen { get; set; }
        public decimal? Sal { get; set; }
        public decimal? Sulfato { get; set; }
        public decimal? Pes_Mue { get; set; }
        public int? Tip_Ph { get; set; }
        public decimal? Ph_Val { get; set; }
        public string? Tela { get; set; }
        public decimal? Can_Jabo { get; set; }
        public string? Fec_Asignacion { get; set; }
        public int? Nro_Tubo { get; set; }
        public int? JabonadoIndex { get; set; }
        public decimal? Ph_Jab2 { get; set; }
        public decimal? Ph_Jab3 { get; set; }
        public decimal? SulfatoReal { get; set; }
        public string? Procedencia { get; set; }
        public string? Fec_Ent { get; set; }
        public string? Familia { get; set; }
        public decimal? Ph_Des { get; set; }
        public int? Tip_Fij { get; set; }
        public int? Fijado { get; set; }
        public string? Descarga { get; set; }
        public decimal? Car_Gr { get; set; }
        public int? Nro_Tubo_Jab { get; set; }
        public int? Ahi_Id_Jab { get; set; }
        public decimal? Ph_Neu { get; set; }
        public int? Previo { get; set; }
        public string? Tip_Ten { get; set; }
        public string? Neutralizado { get; set; }
        public string? Fij_Tip_Des { get; set; }
        public string? dosificacion1Estado { get; set; }
        public string? dosificacion2Estado { get; set; }
        public string? dosificacion3Estado { get; set; }
        public string? sodaEstado { get; set; }
        public int? Nro_Dsf { get; set; }
        public string? Est_Dsf { get; set; }
        public DateTime? Fec_Ini_Tenido { get; set; }
        public DateTime? Fec_Fin_Tenido { get; set; }
        public DateTime? Fec_Ini_Tenido_2 { get; set; }
        public DateTime? Fec_Fin_Tenido_2 { get; set; }

        
        //PRODUCCION
        public string? Cod_OrdTra { get; set; }
        public string? Partidas { get; set; }

        //NUEVOS CAMPOS
        public string? Ahi_Des { get; set; }
        public string? des_Colorante_Optico { get; set; }
        public string? Cod_Colorante_Optico { get; set; }
        public decimal? Can_Colorante_Optico { get; set; }

        //NUEVOS CAMPOS REPORTE 
        public int Maq_Nro_Tubo { get; set; }
        public string? Maq_Ahi_Des { get; set; }
        public int Jab_Nro_Tubo { get; set; }
        public string? Jab_Ahi_Des { get; set; }
        //NUEVO CAMPO 
        public DateTime? Fec_Env_Autolab { get; set; }
        public DateTime? Fec_Teorico_Inicio_Tenido { get; set; }
        public DateTime? Fecha_AsignaAnalista { get; set; }

        //CAMPOS DE FECHAS DE JABONADOS
        public DateTime? Fecha_Jabonado_Inicio_1 { get; set; }
        public DateTime? Fecha_Jabonado_Fin_1 { get; set; }
        public DateTime? Fecha_Jabonado_Inicio_2 { get; set; }
        public DateTime? Fecha_Jabonado_Fin_2 { get; set; }
        public string? Proceso_Des { get; set; }

        public int Jab_Nro_Tubo_2 { get; set; }
        public string? Jab_Ahi_Des_2 { get; set; }
        public int Jab_Nro_Tubo_3 { get; set; }
        public string? Jab_Ahi_Des_3 { get; set; }

        public string? Nombre_Cliente { get; set; }
        public string? Nombre_Analista { get; set; }

    }
}
