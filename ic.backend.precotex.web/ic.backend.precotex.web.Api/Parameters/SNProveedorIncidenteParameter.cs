using System;

namespace ic.backend.precotex.web.Api.Parameters
{
    public class SNProveedorIncidenteParameter
    {
        public string? Accion { get; set; } // 'I', 'U', 'E'
        public int Id_Incidente { get; set; }
        public int Id_Proveedor { get; set; }
        public int Id_Area { get; set; }
        public DateTime? Fec_Incidente { get; set; }
        public string? Des_Producto { get; set; }
        public string Des_Incidente { get; set; } = string.Empty;
        public string? Des_Acciones { get; set; }
        public bool Flg_Cerrado { get; set; }
        public DateTime? Fec_Cierre { get; set; }
        public string? Cod_Usuario { get; set; }
    }
}
