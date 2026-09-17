using System;

namespace ic.backend.precotex.web.Api.Parameters
{
    public class SNIndicadorMedicionParameter
    {
        public int? Id_Medicion { get; set; }
        public int? Id_Indicador { get; set; }
        public string? Accion { get; set; }
        public string? Codigo_Indicador { get; set; }
        public string? Nombre_Indicador { get; set; }
        public string? Indicador { get; set; }
        public string? Tipo { get; set; }
        public string? Sede { get; set; }
        public string? Proceso { get; set; }
        public string? Nombre_Proceso { get; set; }
        public string? Norma { get; set; }
        public string? Frecuencia { get; set; }
        public decimal? Meta { get; set; }
        public string? Periodo { get; set; }
        public decimal? Valor_Obtenido { get; set; }
        public string? Semaforo { get; set; }
        public string? Evidencia { get; set; }
        public string? Archivo_Evidencia { get; set; }
        public string? Comentario { get; set; }
        public string? Usuario_Registro { get; set; }
    }
}
