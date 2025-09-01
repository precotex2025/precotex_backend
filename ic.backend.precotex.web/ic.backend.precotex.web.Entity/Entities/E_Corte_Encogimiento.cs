using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities
{
    public class E_Corte_Encogimiento
    {
        public int? Id_Corte { get; set; }
        public string? Cod_Partida { get; set; }
        public int? Num_Secuencia { get; set; }
        public string? Nom_Cliente { get; set; }
        public string? Cod_Tela { get; set; }
        public string? Des_Tela { get; set; }
        public string? Des_Color { get; set; }
        public decimal? Ancho_Real_cm { get; set; }
        public string? Encogimiento_AnchoA { get; set; }
        public string? Encogimiento_LargoA { get; set; }
        public string? Encogimiento_Ancho_Lab { get; set; }
        public string? Encogimiento_Largo_Lab { get; set; }
        public decimal? Ancho_Antes_Lav { get; set; }
        public decimal? Alto_Antes_Lav { get; set; }
        public decimal? Ancho_Despues_Lav { get; set; }
        public decimal? Alto_Despues_Lav { get; set; }
        public DateTime? Fecha_Creacion { get; set; }
        public bool? Check_Seleccionado { get; set; }
        public decimal? Sesgadura { get; set; }
        public decimal? Encogimiento_Ancho { get; set; }
        public decimal? Encogimiento_Largo { get; set; }
        public byte? Estado_Medida_Antes { get; set; }
        public byte? Estado_Medida_Despues { get; set; }

    }
}
