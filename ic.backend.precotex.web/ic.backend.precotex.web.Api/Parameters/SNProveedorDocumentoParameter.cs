using System;

namespace ic.backend.precotex.web.Api.Parameters
{
    public class SNProveedorDocumentoParameter
    {
        public string? Accion { get; set; } // 'I', 'U', 'E'
        public int Id_Documento { get; set; }
        public int Id_Proveedor { get; set; }
        public int Id_Tipo_Doc { get; set; }
        public string? Ref_Persona { get; set; }
        public string? Num_Documento { get; set; }
        public DateTime? Fec_Emision { get; set; }
        public DateTime? Fec_Vencimiento { get; set; }
        public string? Ruta_Adjunto { get; set; }
        public string? Observaciones { get; set; }
        public string? Cod_Usuario { get; set; }
    }
}

