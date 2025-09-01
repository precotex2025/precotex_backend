using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities
{
    public class Tx_TelaMed
    {
        public string? Cod_Tela { get; set; }
        public string? Cod_Medida { get; set; }
        public string? Descripcion { get; set; }
        public string? Largo { get; set; }
        public string? Alto { get; set; }
        public int?  Agujas { get; set; }
        public int? Pasadas { get; set; }
        public string? Tipo_Medida { get; set; }
        public int? Peso { get; set; }
        public decimal? LargoCrudo { get; set; }
        public decimal? AltoCrudo { get; set; }
        public decimal? LargoReal { get; set; }
        public decimal? AltoReal { get; set; }
        public int Id { get; set; }
        public string? Tipo { get; set; }
        public int? Estado { get; set; }

        /*
          
    Cod_Tela	Cod_Tela
    Cod_Medida	Cod_Talla
    Descripcion	varchar
    Largo	varchar
    Alto	varchar
    Agujas	int
    Pasadas	int
    Tipo_Medida	char
    Peso	numeric
    LargoCrudo	decimal
    AltoCrudo	decimal      
         
         */
    }
}
