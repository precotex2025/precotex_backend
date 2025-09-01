using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities
{
    public  class Tx_Ubicacion
    {
        public int? Id_ubicacion { get; set; }
        public string? Cod_ubicacion { get; set; }
        public string? Tipo { get; set; }
        public int? Num_Tipo { get; set; }
        public string? Piso { get; set; }
        public int? Num_Nicho { get; set; }
        public int? Capacidad { get; set; }
        public string? Cod_Equipo { get; set; }
        public DateTime? Fec_Creacion { get; set; }
        public string? Cod_usuario { get; set; }

        //Otros
        public int? Capa_Total_Grupos { get; set; }
        public int? Capa_Total_Bolsas { get; set; }


    }
}
