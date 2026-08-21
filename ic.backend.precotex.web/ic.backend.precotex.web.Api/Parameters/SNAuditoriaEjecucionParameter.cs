using System;

namespace ic.backend.precotex.web.Api.Parameters
{
    public class SNAuditoriaEjecucionParameter
    {
        public string? Accion { get; set; }
        public int? Id_Ejecucion { get; set; }
        public string? Codigo_Ejecucion { get; set; }
        public string? Codigo_Auditoria { get; set; }
        public string? Auditoria { get; set; }
        public DateTime? Fecha_Ejecucion { get; set; }
        public string? Fecha { get; set; }
        public string? Auditados { get; set; }
        public string? Tipo_Hallazgo { get; set; }
        public string? Tipo { get; set; }
        public string? Descripcion_Hallazgo { get; set; }
        public string? Descripcion { get; set; }
        public string? Codigo_NC { get; set; }
        public string? Nc { get; set; }
        public string? Responsable_Auditor { get; set; }
        public string? Responsable { get; set; }
        public string? Estado { get; set; }
        public string? Ruta_Archivo_Evidencia { get; set; }
        public string? Archivo { get; set; }
        public string? Notas_Adicionales { get; set; }
        public string? Notas { get; set; }
        public string? Cod_Usuario { get; set; }
    }
}
