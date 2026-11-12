using System;

namespace ic.backend.precotex.web.Entity.Entities.SecureNorm
{
    public class SN_Proveedor_HistorialCambio
    {
        public int Id_Proveedor { get; set; }
        public string Razon_Social { get; set; } = string.Empty;
        public string? Ruc { get; set; }
        public bool Flg_Activo { get; set; }
        public bool Flg_Homologado { get; set; }
        public string? Cod_Usuario_Modifica { get; set; }
        public DateTime? Fec_Modifica { get; set; }
        public string Tipo_Accion { get; set; } = string.Empty;
    }
}
