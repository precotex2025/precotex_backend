using System;

namespace ic.backend.precotex.web.Entity.Entities.SecureNorm
{
    public class SN_No_Conformidad
    {
        public int Id_No_Conformidad { get; set; }
        public string NC { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string Accion { get; set; } = string.Empty;
        public string Proceso { get; set; } = string.Empty;
        public string Responsable { get; set; } = string.Empty;
        public DateTime? Fecha_Inicio { get; set; }
        public DateTime? Fecha_Limite { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Codigo_Auditoria { get; set; } = string.Empty;
        public string Usuario_Registro { get; set; } = string.Empty;
        public DateTime? Fecha_Registro { get; set; }
        public bool? flg_Activo { get; set; }

        // Campo JOIN opcional
        public string? Auditoria_Norma { get; set; }
    }
}
