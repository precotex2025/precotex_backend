using System;

namespace ic.backend.precotex.web.Api.Parameters
{
    public class SNNoConformidadParameter
    {
        public string? Accion { get; set; }
        public string? NC { get; set; }
        public string? Tipo { get; set; }
        public string? Accion_Desc { get; set; }
        public string? Proceso { get; set; }
        public string? Responsable { get; set; }
        public DateTime? Fecha_Inicio { get; set; }
        public DateTime? Fecha_Limite { get; set; }
        public string? Estado { get; set; }
        public string? Descripcion { get; set; }
        public string? Codigo_Auditoria { get; set; }
        public string? Cod_Usuario { get; set; }
    }
}
