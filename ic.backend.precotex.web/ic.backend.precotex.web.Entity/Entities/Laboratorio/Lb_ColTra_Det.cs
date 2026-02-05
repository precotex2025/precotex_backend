using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Laboratorio
{
    public class Lb_ColTra_Det
    {
        public int Corr_Carta {  get; set; }
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
    }
}
