using System;

namespace ic.backend.precotex.web.Entity.Entities.SecureNorm
{
    public class SN_Proveedor_Incidente
    {
        public int Id_Incidente { get; set; }
        public int Id_Proveedor { get; set; }
        public string? Des_Proveedor { get; set; }
        public int Id_Area { get; set; }
        public string? Des_Area { get; set; }
        public DateTime? Fec_Incidente { get; set; }
        public int Num_Ciclo { get; set; }
        public string? Des_Producto { get; set; }
        public string Des_Incidente { get; set; } = string.Empty;
        public string? Des_Acciones { get; set; }
        public bool Flg_Cerrado { get; set; }
        public DateTime? Fec_Cierre { get; set; }
        public string? Cod_Usuario_Reg { get; set; }
        public DateTime? Fec_Registro { get; set; }
    }
}
