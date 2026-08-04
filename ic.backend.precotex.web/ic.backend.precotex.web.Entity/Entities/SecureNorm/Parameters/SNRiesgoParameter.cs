using System;

namespace ic.backend.precotex.web.Entity.Entities.SecureNorm.Parameters
{
    public class SNRiesgoParameter
    {
        public string? Accion { get; set; }
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
    }
}
