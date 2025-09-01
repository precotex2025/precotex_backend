using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities
{
    public class Tx_Bulto_Hilado
    {
        public string? Num_Corre { get; set; }
        public int? Cantidad_Cono { get; set; }
        public string? Cod_OrdProv { get; set; }
        public string? Num_Semana { get; set; }
        public string? Nom_Conera { get; set; }
        public decimal? Peso_Neto { get; set; }

        //Otros
        public string? Grupo { get; set; }
        public string? Cod_ubicacion { get; set; }
    }
}
