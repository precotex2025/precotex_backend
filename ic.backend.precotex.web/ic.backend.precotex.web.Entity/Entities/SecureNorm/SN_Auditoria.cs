using System;

namespace ic.backend.precotex.web.Entity.Entities.SecureNorm
{
    public class SN_Auditoria
    {
        public int Id_Auditoria { get; set; }
        public string? Codigo_Auditoria { get; set; }
        public string? Tipo { get; set; }
        public string? Norma { get; set; }
        public string? Responsable { get; set; }
        public string? Areas { get; set; }
        public DateTime? Fecha_Inicio { get; set; }
        public DateTime? Fecha_Fin { get; set; }
        public string? Frecuencia { get; set; }
        public string? Alcance { get; set; }
        public string? Estado { get; set; }
        public string? Usuario_Registro { get; set; }
        public DateTime? Fecha_Registro { get; set; }
        public bool? flg_Activo { get; set; }
    }
}
