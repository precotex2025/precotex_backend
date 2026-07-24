using System;

namespace ic.backend.precotex.web.Entity.Entities.SecureNorm
{
    public class SN_Riesgo
    {
        public int Id_Riesgo { get; set; }
        public string? Codigo { get; set; }
        public string? Tipo { get; set; }
        public string? Descripcion_Breve { get; set; }
        public string? Proceso { get; set; }
        public string? Nivel { get; set; }
        public string? Estado { get; set; }
        public string? Responsable { get; set; }
        public DateTime? Fecha_Revision { get; set; }
        public string? Medida_Control { get; set; }
        public string? Usuario_Registro { get; set; }
        public DateTime? Fecha_Registro { get; set; }
        public bool? Flg_Activo { get; set; }
        public string? Codigo_Proceso { get; set; }
    }
}
