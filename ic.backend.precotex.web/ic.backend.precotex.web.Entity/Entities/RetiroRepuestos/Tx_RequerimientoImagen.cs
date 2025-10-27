using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.RetiroRepuestos
{
    public class Tx_RequerimientoImagen
    {
        public string? nNum_Requerimiento { get; set; }
        public string? nNum_Secuencia { get; set; }
        public string? nCan_Requerida { get; set; }
        public string? sRpt_Cambio { get; set; }
        public string? sNombre_Archivo { get; set; }
        public string? ImagenBase64 { get; set; }
        public string? UrlImagen { get; set; }
    }
}
