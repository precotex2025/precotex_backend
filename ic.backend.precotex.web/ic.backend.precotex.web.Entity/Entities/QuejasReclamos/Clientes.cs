using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.QuejasReclamos
{
    public class Clientes
    {
        public class Cliente
        {
            public string Cod_Cliente_Tex { get; set; } = string.Empty;
            public string Nom_Cliente { get; set; } = string.Empty;
            public string Abr_Cliente { get; set; } = string.Empty;

        }
    }
}
