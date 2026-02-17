using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Personas
{
    public class Tx_Personas
    {
        public string? Nro_Dni { get; set; }
        public string? Descripcion { get; set; }
        public int? Foto_Id { get; set; }
        public string? Foto_Nro_Dni { get; set; }
        public string? Foto_Ruta { get; set; }
        public string? Usr_Reg { get; set; }
        public string? Fec_Reg { get; set; }
        public string? Usr_Mod { get; set; }
        public string? Fec_Mod { get; set; }
        public string? FotoBase64 { get; set; }
    }
}
