using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities
{
    public class Tx_Ubicacion_Impresora_Activa
    {
        public int? Id_Tx_Ubicacion_Impresora_Activa { get; set; }
        public string? NombreImpresora { get; set; }
        public string? NombreUbicacion { get; set; }
        public string? Flg_Predeterminado { get; set; }
        public DateTime? Fec_Registro { get; set; }
        public string? Usu_Registro { get; set; }
        public string? Cod_Equipo { get; set; }        
    }
}
