using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities
{
    public class Tx_Colgador_Registro_Det
    {
        /*Id Unicos*/
        public int Id_Tx_Colgador_Registro_Det { get; set; }
        public int Id_Tx_Colgador_Registro_Cab { get; set; }

        /*Medidas*/
        public decimal? Encog_Largo { get; set; }
        public decimal? Encog_Ancho { get; set; }
        public decimal? Ancho_Acabado { get; set; }
        public decimal? Ancho_Lavado { get; set; }
        public decimal? Gramaje_Acab { get; set; }
        public decimal? Gramaje_Comercial { get; set; }
        public decimal? Rendimiento { get; set; }
        public int? Diametro { get; set; }

        /*Descripciones*/
        public string? Des_Galga { get; set; }
        public string? Des_Color { get; set; }
        public string? Des_Fabric_Finish { get; set; }
        public string? Des_Fabric_Wash { get; set; }
        public string? Glosa { get; set; }

        public string? Flg_Estatus { get; set; }

    }
}
