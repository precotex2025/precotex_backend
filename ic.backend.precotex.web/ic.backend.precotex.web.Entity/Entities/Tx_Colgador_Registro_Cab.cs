using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities
{
    public class Tx_Colgador_Registro_Cab
    {
        public int? Id_Tx_Colgador_Registro_Cab { get; set; }
        public string? Cod_Tela { get; set; }
        public string? Cod_OrdTra { get; set; }
        public string? Cod_Ruta { get; set; }
        public string? Cod_Cliente_Tex { get; set; }
        public string? Fabric { get; set; }
        public string? Yarn { get; set; }
        public string? Composicion { get; set; }
        public string? Flg_Estatus { get; set; }
        public DateTime? Fec_Registro { get; set; }
        public string? Usu_Registro { get; set; }
        public DateTime? Fec_Modifica { get; set; }
        public string? Usu_Modifica { get; set; }
        public string? Cod_Equipo { get; set; }

        //Otros
        public string? Nom_Cliente { get; set; }
        public string? CodigoBarra { get; set; }

    }
}
