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
        public string? Descripcion_Color { get; set; }
        public string? Cod_Color {  get; set; }
        public string? Estandar_Tono_Comer {  get; set; }
        public string? Formulado { get; set; }
    }       
}
