using System;

namespace ic.backend.precotex.web.Api.Parameters
{
    public class SNIndicadorMedicionParameter
    {
        public string? Accion { get; set; }
        public int? Id_Medicion { get; set; }
        public int? Id_Indicador { get; set; }
        public string? Codigo_Indicador { get; set; }
        public string? Periodo { get; set; }
        public decimal? Valor_Obtenido { get; set; }
        public string? Comentario { get; set; }
        public string? Usuario_Registro { get; set; }
    }
}
