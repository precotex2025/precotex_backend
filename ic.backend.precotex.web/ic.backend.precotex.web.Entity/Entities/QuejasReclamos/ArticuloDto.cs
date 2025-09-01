using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.QuejasReclamos
{
    public class ArticuloDto
    {
        public string Cod_Tela { get; set; } = string.Empty;
        public string Des_Tela { get; set; } = string.Empty;
        public int? Num_Secuencia { get; set; }

        public string Cod_Color { get; set; } = string.Empty;
        public string Des_Color { get; set; } = string.Empty;

        public string Cod_Cliente_Tex { get; set; } = string.Empty;
        public string Nom_Cliente { get; set; } = string.Empty;

        public int Id_Unidad_NegocioKey { get; set; }
        public string Des_Unidad_NegocioKey { get; set; } = string.Empty;


    }
}
