using System;

namespace ic.backend.precotex.web.Entity.Entities.SecureNorm
{
    public class SN_Indicador_Medicion
    {
        public int Id_Medicion { get; set; }
        public int Id_Indicador { get; set; }
        public string? Codigo_Indicador { get; set; }
        public string? Nombre_Indicador { get; set; }
        public string? Nombre_Proceso { get; set; }
        public decimal Meta { get; set; }
        public string? Unidad_Medida { get; set; }
        public string Periodo { get; set; } = string.Empty;
        public decimal Valor_Obtenido { get; set; }
        public string? Comentario { get; set; }
        public string? Usuario_Registro { get; set; }
        public DateTime? Fecha_Registro { get; set; }
        public string? Semaforo { get; set; }
    }
}
