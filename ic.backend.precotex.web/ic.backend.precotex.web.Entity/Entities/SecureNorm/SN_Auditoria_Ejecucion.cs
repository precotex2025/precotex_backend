using System;

namespace ic.backend.precotex.web.Entity.Entities.SecureNorm
{
    public class SN_Auditoria_Ejecucion
    {
        public int Id_Ejecucion { get; set; }
        public string Codigo_Ejecucion { get; set; } = string.Empty;
        public string Codigo_Auditoria { get; set; } = string.Empty;
        public string Norma { get; set; } = string.Empty;
        public string Proceso { get; set; } = string.Empty;
        public string Fecha { get; set; } = string.Empty;
        public DateTime? Fecha_Ejecucion { get; set; }
        public string Auditados { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string Tipo_Hallazgo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Descripcion_Hallazgo { get; set; } = string.Empty;
        public string Nc { get; set; } = string.Empty;
        public string Codigo_NC { get; set; } = string.Empty;
        public string Responsable { get; set; } = string.Empty;
        public string Responsable_Auditor { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Archivo { get; set; } = string.Empty;
        public string Ruta_Archivo_Evidencia { get; set; } = string.Empty;
        public string Notas { get; set; } = string.Empty;
        public string Notas_Adicionales { get; set; } = string.Empty;
        public string Cod_Usuario { get; set; } = string.Empty;
    }
}
