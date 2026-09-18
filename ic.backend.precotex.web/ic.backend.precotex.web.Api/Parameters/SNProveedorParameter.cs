using System;

namespace ic.backend.precotex.web.Api.Parameters
{
    public class SNProveedorParameter
    {
        public string? Accion { get; set; } // 'I', 'U', 'D'
        public int Id { get; set; }
        public string? Razon { get; set; }
        public string? Ruc { get; set; }
        public string? Tipo { get; set; }
        public string? Proceso { get; set; }
        public string? Contacto { get; set; }
        public string? Homologacion { get; set; }
        public string? Desempeno { get; set; }
        public string? Evaluacion { get; set; }
        public string? Reeval { get; set; }
        public string? Sctr { get; set; }
        public string? Induccion { get; set; }
        public string? Iperc { get; set; }
        public string? Seguro { get; set; }
    }
}