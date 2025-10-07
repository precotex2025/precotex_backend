using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Laboratorio
{
    public class Lb_ColTra_Cab_y_Det
    {
        public string? Cod_Cliente { get; set; }
        public string? Des_Cliente { get; set; }
        public int? Corr_Carta { get; set; }
        public int? Sec { get; set; }
        public string? Cod_Tela { get; set; }
        public string? Des_Tela { get; set; }
        public DateTime? Fec_creacion { get; set; }
        public string? Lab_Dias { get; set; }
        public DateTime? Fec_Entrega { get; set; }
        public DateTime? Fec_compromiso { get; set; }
        public int? Dias_Falt_Compromiso { get; set; }
        public string? Estado { get; set; }
        public string? Descripcion_Color { get; set; }
        public string? Cod_Color { get; set; }
        public string? Estandar_Tono_Comer { get; set; }
        public string? Formulado { get; set; }
        public string? Flg_Est_Lab { get; set; }
        public string? Tipo { get; set; }
        public string? Cod_Ruta { get; set; }
    }
}
