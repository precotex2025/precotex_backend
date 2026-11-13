using System;

namespace ic.backend.precotex.web.Entity.Entities.SecureNorm
{
    public class SN_Proveedor_Documento
    {
        public int Id_Documento { get; set; }
        public int Id_Proveedor { get; set; }
        public string? Des_Proveedor { get; set; }
        public int Id_Tipo_Doc { get; set; }
        public string? Des_Tipo { get; set; }
        public string? Ref_Persona { get; set; }
        public string? Num_Documento { get; set; }
        public DateTime? Fec_Emision { get; set; }
        public DateTime? Fec_Vencimiento { get; set; }
        public string? Estado_Vigencia { get; set; }
        public string? Ruta_Adjunto { get; set; }
        public string? Observaciones { get; set; }
        public string? Cod_Usuario { get; set; }
    }

    public class SN_Proveedor_TipoDocumento
    {
        public int Id_Tipo_Doc { get; set; }
        public string Des_Tipo { get; set; } = string.Empty;
        public int Num_Dias_Alerta { get; set; }
        public bool Flg_Por_Persona { get; set; }
    }
}
