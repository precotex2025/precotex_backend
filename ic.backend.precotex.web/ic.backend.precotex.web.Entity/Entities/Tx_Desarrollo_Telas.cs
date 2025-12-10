using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities
{
    public class Tx_Desarrollo_Telas
    {
        public string? Cod_Tela { get; set; }
        public string? Des_Tela { get; set; }
        public string? Des_Motivo_Solicitud { get; set; }
        public string? Comentario_Solicitud { get; set; }
        public string? Cod_Version { get; set; }
        public string? Nom_Version { get; set; }
        public string? Comentario { get; set; }
        public string? Ruta_Archivo { get; set; }
        public string? Ruta_Archivo_Ant { get; set; }
        public DateTime? Fec_Registro_Solicitud { get; set; }
        public string? Cod_Usuario_Solicitud { get; set; }
    }
}
