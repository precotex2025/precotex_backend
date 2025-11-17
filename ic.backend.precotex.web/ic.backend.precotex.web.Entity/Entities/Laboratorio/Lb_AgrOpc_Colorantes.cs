using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ic.backend.precotex.web.Entity.Entities.Laboratorio
{
    public class Lb_AgrOpc_Colorantes
    {
        public int? Corr_Carta { get; set; }
        public int? Sec { get; set; }
        public string? Procedencia { get; set; }
        public int? Correlativo { get; set; }
        public int? id_secuencia { get; set; }
        public string? Col_Cod { get; set; }
        public double? Por_Ini { get; set; }
        public double? Por_Aju { get; set; }
        public double? Por_Fin { get; set; }
        public int? Can_Jabo { get; set; }
        public string? Cur_Jabo { get; set; }
        public string? Fijado { get; set; }
        public string? Acidulado { get; set; }
        public double? Rel_Ban {  get; set; }
        public double? Pes_Mue { get; set; }
        public double? Volumen { get; set; }
        public double? Car_Gr { get; set; }
        public double? Car_Por { get; set; }
        public double? Sod_Gr { get; set; }
        public double? Sod_Por { get; set; }
        public string? Flg_Est_Lab { get; set; }
        public string? Flg_Est_Autolab { get; set; }
        public int? Ahi_Id { get; set; }
        public string? Familia { get; set; }
        public int? Posicion { get; set; }
        /*MOSTRAR EN DETALLE RECETA*/
        public string? Tip_Tenido { get; set; }
        public string? Cur_Tenido { get; set; }
        public string? Previo { get; set; }
        public decimal? R_B { get; set; }
        public decimal? Ph_Ini { get; set; }
        public decimal? Ph_Fin { get; set; }
        public decimal? Ph_Jab { get; set; }
        /***************************/
        public IEnumerable<Colorantes>? Colorantes { get; set; }
        public IEnumerable<Auxiliares>? Auxiliares { get; set; }
        
    }

    public class Auxiliares
    {
        public int? Corr_Carta { get; set; }
        public int? Sec { get; set; }
        public string? Col_Cod { get; set; }
        public string? Col_Des { get; set; }
        public decimal? Por_Ini { get; set; }
        public decimal? Por_Aju { get; set; }
        public decimal? Por_Fin { get; set; }
        public int? id_secuencia { get; set; }
        public int? correlativo { get; set; }
    }

    public class Colorantes
    {
        public int? Corr_Carta { get; set; }
        public int? Sec { get; set; }
        public string? Col_Cod { get; set; }
        public string? Col_Des { get; set; }
        public decimal? Por_Ini { get; set; }
        public decimal? Por_Aju { get; set; }
        public decimal? Por_Fin { get; set; }
        public int? id_secuencia { get; set; }
        public int? correlativo { get; set; }
    }


}
