using System;

namespace ic.backend.precotex.web.Entity.Entities.SecureNorm
{
    public class SN_Mejora
    {
        public int Id_Mejora { get; set; }
        public string? Codigo { get; set; }
        public string? Fuente { get; set; }
        public string? Codigo_Proceso { get; set; }
        public string? Descripcion { get; set; }
        public string? Responsable { get; set; }
        public DateTime? Fecha_Inicio { get; set; }
        public DateTime? Fecha_Fin_Estimada { get; set; }
        public DateTime? Fecha_Fin { get; set; }
        public string? Estado { get; set; }
        public string? Sede { get; set; }
        public string? Herramienta { get; set; }
        public string? Proveniente { get; set; }
        public int? Cumplimiento { get; set; }
        public string? Archivo { get; set; }
        public string? Usuario_Registro { get; set; }
        public DateTime? Fecha_Registro { get; set; }
        public bool? flg_Activo { get; set; }

        public string? Nombre_Proceso { get; set; }
    }
}
