using System;

namespace ic.backend.precotex.web.Api.Parameters
{
    public class SNIndicadorParameter
    {
        public string? Accion { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public string? Codigo_Proceso { get; set; }
        public string? Unidad_Medida { get; set; }
        public decimal? Meta { get; set; }
        public string? Frecuencia { get; set; }
        public string? Usuario_Registro { get; set; }
    }
}
